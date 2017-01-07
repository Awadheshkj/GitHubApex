#Region "  Includes  "

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports clsDatabaseHelper
Imports System.Configuration
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Net.Mail
Imports System.Exception

#End Region

Public Class clsMain

    Public Shared PasswordPrefix As String = Chr(200)
    Public Shared curl As String
    Public Shared CookieName As String = "DASTC"

    Public Class clsConfiguration

        Public Sub New()
            TableName = "APEX_Login"    ' Main User Master - REQUIRED
            UserID = "RefUserDetailsID"         ' Field name for UserID (PK) - REQUIRED
            EmailID = "EmailID"       ' Field Name for Email ID - Leave Blank if not available

            ' Set any one of the following (ordered by priority)
            RealName = "DisplayName"
            FirstName = "FirstName"
            LastName = "LastName"

            ' Following two fields are Compulsory
            Username = "Username"
            Password = "Password"

            ' Whichever not applicable, leave blank
            IsActive = "IsActive"
            IsDeleted = "IsDeleted"
            LoginAllowed = "IsLoginAllowed"

        End Sub

        ' MAIN USER TABLE
        Public TableName As String
        Public UserID As String
        Public EmailID As String

        ' Set any one of the following (ordered by priority)
        Public RealName As String
        Public FirstName As String
        Public LastName As String

        Public Username As String
        Public Password As String

        ' Whichever not applicable, leave blank
        Public IsActive As String
        Public IsDeleted As String
        Public LoginAllowed As String


    End Class

    Public Shared config As clsConfiguration = New clsConfiguration

    Enum _UserLevels
        _Default
        Administrator
        Member
        Visitor
    End Enum
    Enum _Module
        Admin
        Trainer
        StudentParents
    End Enum

    Enum FileType
        Image
        Pdf
        Word
        Excel
        PPT
        Zip
        Video
        Audio
        Other
        Hyperlink
    End Enum

    Enum _IconType
        _Default
        iconError
        iconWarning
        iconInfo
        iconQuestion
        iconSuccess
    End Enum


    Enum _ReturnCodes

        AccessGranted = 1
        RecordSaved
        RecordDeleted
        Activated
        DeActivated
        PasswordChanged

        ' Error Codes
        InValidUsername = -99
        InValidPassword
        AccountInActive
        Account_in_RecycleBin
        LoginNotAllowed
        AccessDenied
        InvalidAction
        RecordNotSaved
    End Enum


#Region "  Authentication  "

    ''' <summary>
    ''' This function will logout the user
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub LogOut()
        HttpContext.Current.Response.Cookies(CookieName).Item(config.UserID) = ""
        HttpContext.Current.Response.Cookies(CookieName).Expires = Now.AddDays(-100)

    End Sub

    ''' <summary>
    ''' This function checks if any user is logged on current machine or not
    ''' </summary>
    ''' <returns>True if user is logged in, False otherwise</returns>
    ''' <remarks>Vikramjit R.Rai (22-May-2010)</remarks>
    Public Shared Function LoggedIn() As Boolean

        Return (Not LoggedOut())

    End Function

    ''' <summary>
    ''' This function checks if any user is logged on current machine or not
    ''' </summary>
    ''' <returns>True if user is logged out, False otherwise</returns>
    ''' <remarks>Vikramjit R.Rai (22-May-2010)</remarks>
    Public Shared Function LoggedOut() As Boolean

        Dim cookie As HttpCookie
        cookie = HttpContext.Current.Request.Cookies(CookieName)

        If cookie Is Nothing Then
            Return True
        Else
            If cookie.Item(config.UserID) = "" Then Return True
            Return False
        End If

    End Function

    ''' <summary>
    ''' This function will compare the password stored in database with password entered by user. It will check whether password in database is encrypted or not.
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <param name="PWDinDB"></param>
    ''' <param name="PWDbyUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MatchPassword(ByVal UserID As Integer, ByVal PWDinDB As String, ByVal PWDbyUser As String) As _ReturnCodes
        Try
            Entering("MatchPassword to Compare Password")

            If Left(PWDinDB, 1) = PasswordPrefix Then
                PWDbyUser = PasswordPrefix & Encrypt(PWDbyUser)
            Else
                EncryptPWD(UserID, PWDinDB)
            End If

            If PWDinDB = PWDbyUser Then
                Leaving("MatchPassword with Success")
                Return _ReturnCodes.AccessGranted
            Else
                Leaving("MatchPassword with Failure")
                Return _ReturnCodes.InValidPassword
            End If
        Catch ex As Exception
            LogError(ex, "", "Region: Authentication", "clsMain", "MatchPassword")
            Leaving("MatchPassword with Error")
            Return _ReturnCodes.InvalidAction
        End Try

        Return _ReturnCodes.AccessDenied

    End Function

    ''' <summary>
    ''' This function will encrypt the password stored in Database
    ''' </summary>
    ''' <param name="UserID">UserID, whose password is to be encrypted</param>
    ''' <param name="PWD">Password that is to be encrypted</param>
    ''' <remarks>VSR</remarks>
    Public Shared Sub EncryptPWD(ByVal UserID As Integer, ByVal PWD As String)
        Entering("EncryptPassword")
        Try
            If PWD = "" Or UserID <= 0 Then Return

            PWD = PasswordPrefix & Encrypt(PWD)
            HttpContext.Current.Trace.Write("** Going to encrypt password in database")
            ExecuteNonQuery("Update " & config.TableName & " set " & config.Password & " = N'" & PWD.Replace("'", "''") & "' where " & config.UserID & " = " & UserID)

        Catch ex As Exception
            LogError(ex, "", "Region: Authentication", "clsMain", "EncryptPWD")
        End Try

        Leaving("EncryptPassword")

    End Sub

    ''' <summary>
    ''' Function to Grant Access to user. It will create the cookies as required by the application
    ''' </summary>
    ''' <param name="Username">Username to be authenticated</param>
    ''' <param name="Password">Password to be authenticated</param>
    ''' <returns>True if User is Authenticated, False otherwise</returns>
    ''' <remarks>VSR - 5-DEC-2007</remarks>
    Public Shared Function requestAccess(ByVal Username As String, ByVal Password As String, Optional ByVal Remember As Boolean = False) As _ReturnCodes

        Entering("requestAccess")

        'If Username.Trim = "" Or Password.Trim = "" Then Return False

        Dim SQL As String
        Dim DR As SqlDataReader

        DR = Nothing
        '  SQL = "Select * from " & config.TableName & " where BINARY_CHECKSUM(" & config.Username & ") = BINARY_CHECKSUM('" & Username.Replace("'", "''") & "') and BINARY_CHECKSUM(" & config.Password & ") = BINARY_CHECKSUM('" & Password.Replace("'", "''") & "')"
        SQL = "Select * from " & config.TableName & " where BINARY_CHECKSUM(" & config.Username & ") = BINARY_CHECKSUM('" & Username.Replace("'", "''") & "')"



        ExecuteReader(SQL, DR)

        If DR.Read Then
            Try

                ' Checking for Password and Other Authorization Settings
                Dim PWD, IsActive, IsDeleted, LoginAllowed As String
                Dim EmployeeID As Integer
                PWD = ""
                IsActive = ""
                IsDeleted = ""
                LoginAllowed = ""

                If Not IsDBNull(DR(config.UserID)) Then
                    EmployeeID = DR(config.UserID)
                Else
                    EmployeeID = -1
                End If

                If Not IsDBNull(DR(config.Password)) Then
                    PWD = DR(config.Password)
                    If MatchPassword(EmployeeID, PWD, Password) <= 0 Then
                        HttpContext.Current.Response.Cookies(CookieName).Item(config.UserID) = ""
                        Leaving("requestAccess with failure due to Invalid Password")
                        Return _ReturnCodes.InValidPassword
                    End If
                Else
                    Return _ReturnCodes.InValidPassword
                End If

                If config.IsActive.Trim <> "" Then
                    If Not IsDBNull(DR(config.IsActive)) Then
                        IsActive = DR(config.IsActive)
                    End If

                    If UCase(IsActive) <> "Y" Then
                        Leaving("requestAccess with Failure due to Inactive Account")
                        Return _ReturnCodes.AccountInActive
                    End If
                End If

                If config.IsDeleted.Trim <> "" Then
                    If Not IsDBNull(DR(config.IsDeleted)) Then
                        IsDeleted = DR(config.IsDeleted)
                    End If

                    If UCase(IsDeleted) = "Y" Then
                        Leaving("requestAccess with Failure due to Deleted Account")
                        Return _ReturnCodes.Account_in_RecycleBin
                    End If
                End If

                If config.LoginAllowed.Trim <> "" Then
                    If Not IsDBNull(DR(config.LoginAllowed)) Then
                        LoginAllowed = DR(config.LoginAllowed)
                    End If

                    If UCase(LoginAllowed) <> "Y" Then
                        Leaving("requestAccess with Failure due to Locked Account")
                        Return _ReturnCodes.LoginNotAllowed
                    End If
                End If

                HttpContext.Current.Response.Cookies(CookieName).Item(config.UserID) = EmployeeID

                If config.RealName.Trim <> "" Then
                    If Not IsDBNull(DR(config.RealName)) Then
                        HttpContext.Current.Response.Cookies(CookieName).Item(config.RealName) = DR(config.RealName)
                    Else
                        HttpContext.Current.Response.Cookies(CookieName).Item(config.RealName) = ""
                    End If
                Else
                    If config.FirstName.Trim <> "" Then
                        If Not IsDBNull(DR(config.FirstName)) Then
                            HttpContext.Current.Response.Cookies(CookieName).Item(config.RealName) = DR(config.FirstName)
                        Else
                            HttpContext.Current.Response.Cookies(CookieName).Item(config.RealName) = ""
                        End If
                    End If
                End If

                


                If config.EmailID.Trim <> "" Then
                    If Not IsDBNull(DR(config.EmailID)) Then
                        HttpContext.Current.Response.Cookies(CookieName).Item(config.EmailID) = DR(config.EmailID)
                    Else
                        HttpContext.Current.Response.Cookies(CookieName).Item(config.EmailID) = ""
                    End If
                End If




            Catch ex As Exception
                HttpContext.Current.Trace.Warn("error")
                LogError(ex)
                Return _ReturnCodes.AccessDenied
            End Try


        DR.Close()
        DR = Nothing

        HttpContext.Current.Response.Cookies(CookieName).Expires = Now.AddMonths(1)

            Leaving("Leaving requestAccess with Granted Access")

        Return _ReturnCodes.AccessGranted
        Else
        DR.Close()
            DR = Nothing
            Leaving("Leaving requestAccess with Access Denied")
        Return _ReturnCodes.AccessDenied
        End If

    End Function

    Public Shared Function getLoggedUserID() As Integer
        Dim cookie As HttpCookie
        cookie = HttpContext.Current.Request.Cookies(CookieName)
        If cookie Is Nothing Then
            Return -1
        Else
            If cookie.Item(config.UserID) = "" Then
                Return -1
            Else
                If IsNumeric(cookie.Item(config.UserID)) Then
                    Return cookie.Item(config.UserID)
                Else
                    Return -1
                End If
            End If
        End If
    End Function

    Public Shared Function getLoggedUserName() As String
        Dim cookie As HttpCookie
        cookie = HttpContext.Current.Request.Cookies(CookieName)
        If cookie Is Nothing Then
            Return ""
        Else
            If cookie.Item(config.RealName) = "" Then
                Return ""
            Else
                Return cookie.Item(config.RealName)
            End If
        End If
    End Function


    Public Shared Function getLoggedUserEmail() As String
        Dim cookie As HttpCookie
        cookie = HttpContext.Current.Request.Cookies(CookieName)
        If cookie Is Nothing Then
            Return ""
        Else
            If cookie.Item(config.EmailID) = "" Then
                Return ""
            Else
                Return UCase(cookie.Item(config.EmailID))
            End If
        End If
    End Function

    Public Shared Function IsPasswordCorrect(ByVal UserID As Integer, ByVal Password As String) As _ReturnCodes

        Dim SQL As String
        Dim DR As SqlDataReader

        DR = Nothing
        SQL = "Select " & config.UserID & ", " & config.Password & ""
        If config.IsActive.Trim <> "" Then SQL &= ", " & config.IsActive
        If config.IsDeleted.Trim <> "" Then SQL &= ", " & config.IsDeleted
        If config.LoginAllowed.Trim <> "" Then SQL &= ", " & config.LoginAllowed
        SQL &= " from " & config.TableName & " where " & config.UserID & " = " & UserID

        ExecuteReader(SQL, DR)

        If DR.Read Then
            Try

                ' Checking for Password and Other Authorization Settings
                Dim PWD, IsActive, IsDeleted, LoginAllowed As String
                Dim EmployeeID As Integer
                PWD = ""
                IsActive = ""
                IsDeleted = ""
                LoginAllowed = ""

                If Not IsDBNull(DR(config.UserID)) Then
                    EmployeeID = DR(config.UserID)
                Else
                    EmployeeID = -1
                End If

                If Not IsDBNull(DR(config.Password)) Then
                    PWD = DR(config.Password)
                    If MatchPassword(EmployeeID, PWD, Password) <= 0 Then
                        Return _ReturnCodes.InValidPassword
                    End If
                Else
                    Return _ReturnCodes.InValidPassword
                End If

                If config.IsActive.Trim <> "" Then
                    If Not IsDBNull(DR(config.IsActive)) Then
                        IsActive = DR(config.IsActive)
                    End If

                    If UCase(IsActive) <> "Y" Then
                        Return _ReturnCodes.AccountInActive
                    End If
                End If

                If config.IsDeleted.Trim <> "" Then
                    If Not IsDBNull(DR(config.IsDeleted)) Then
                        IsDeleted = DR(config.IsDeleted)
                    End If

                    If UCase(IsDeleted) = "Y" Then
                        Return _ReturnCodes.Account_in_RecycleBin
                    End If
                End If
                
                If config.LoginAllowed.Trim <> "" Then
                    If Not IsDBNull(DR(config.LoginAllowed)) Then
                        LoginAllowed = DR(config.LoginAllowed)
                    End If

                    If UCase(LoginAllowed) <> "Y" Then
                        Return _ReturnCodes.LoginNotAllowed
                    End If
                End If
                
            Catch ex As Exception
            End Try
        End If
        Return _ReturnCodes.AccessGranted
    End Function

#End Region


#Region "  Security  "

    ''' <summary>
    ''' This function will Clean the Input data and replace Single Quotes and "SCRIPT" tag and make it WEB Save and prevent Hacking
    ''' </summary>
    ''' <param name="SourceData">Variable to be replaced</param>
    ''' <returns>Cleaned Data</returns>
    ''' <remarks></remarks>
    Public Shared Function Clean(ByVal SourceData As String, Optional ByVal HTMLAllowed As Boolean = True, Optional ByVal scriptTagAllowed As Boolean = False) As String
        Dim SRC As String
        SRC = SourceData
        ' Replace single quote
        SRC = SRC.Replace("'", "''")

        If HTMLAllowed = False Then
            SRC = HttpContext.Current.Server.UrlEncode(SRC)
        Else
            If scriptTagAllowed = False Then
                ' Replace SCRIPT Tag
                SRC = Replace(SRC, "<script", "&lt;script", , , CompareMethod.Text)
                SRC = Replace(SRC, "</script", "&lt;/script", , , CompareMethod.Text)

                SRC = Replace(SRC, "<form", "&lt;form", , , CompareMethod.Text)
                SRC = Replace(SRC, "</form", "&lt;/form", , , CompareMethod.Text)

                SRC = Replace(SRC, "<input", "&lt;input", , , CompareMethod.Text)
                SRC = Replace(SRC, "</input", "&lt;/input", , , CompareMethod.Text)

                SRC = Replace(SRC, "<embed", "&lt;embed", , , CompareMethod.Text)
                SRC = Replace(SRC, "</embed", "&lt;/embed", , , CompareMethod.Text)

                SRC = Replace(SRC, "<textarea", "&lt;textarea", , , CompareMethod.Text)
                SRC = Replace(SRC, "</textarea", "&lt;/textarea", , , CompareMethod.Text)

                SRC = Replace(SRC, "<select", "&lt;select", , , CompareMethod.Text)
                SRC = Replace(SRC, "</select", "&lt;/select", , , CompareMethod.Text)


            End If
        End If


        Return SRC
    End Function

    Public Shared Function Encrypt(ByVal PlainText As String) As String

        Entering("Encrypt with Value " & PlainText)

        Dim CypherText As String
        Dim signFlag As Boolean
        Dim additive As Integer
        Dim Character As Char
        additive = 0
        signFlag = True
        CypherText = ""
        Character = ""
        HttpContext.Current.Trace.Write("Recieved Plain Text [ " & PlainText & " ] of length " & PlainText.Length)
        For i As Integer = 0 To PlainText.Length - 1


            If (i Mod 10) = 1 Then
                additive += 1
            Else
                additive -= 1
            End If

            If signFlag Then
                additive *= -1
            End If


            HttpContext.Current.Trace.Write("Parsing Step " & i & " with Additive " & additive)

            Character = Chr(Asc(PlainText.Substring(i, 1)) + additive)
            HttpContext.Current.Trace.Warn("Character [" & PlainText.Substring(i, 1) & "] Encoded to : " & Character)
            CypherText &= Character

        Next

        HttpContext.Current.Trace.Warn("Returning Cypher Text: " & CypherText & " of Length " & CypherText.Length)

        Leaving("Encrypt")

        Return CypherText
    End Function

    Public Shared Function Decrypt(ByVal CypherText As String) As String

        Entering("Decrypt with Value " & CypherText)

        Dim PlainText As String
        Dim signFlag As Boolean
        Dim additive As Integer
        Dim Character As Char
        additive = 0
        signFlag = True
        PlainText = ""
        Character = ""
        HttpContext.Current.Trace.Write("Recieved Cypher Text [ " & CypherText & " ] of length " & CypherText.Length)
        For i As Integer = 0 To CypherText.Length - 1


            If (i Mod 10) = 1 Then
                additive -= 1
            Else
                additive += 1
            End If

            If signFlag Then
                additive *= -1
            End If


            HttpContext.Current.Trace.Write("Parsing Step " & i & " with Additive " & additive)

            Character = Chr(Asc(CypherText.Substring(i, 1)) + additive)
            HttpContext.Current.Trace.Warn("Character [" & CypherText.Substring(i, 1) & "] Decoded to : " & Character)
            PlainText &= Character

        Next

        HttpContext.Current.Trace.Warn("Returning Plain Text: " & PlainText & " of Length " & PlainText.Length)

        Leaving("Decrypt")

        Return PlainText

    End Function


#End Region


#Region "  Global Processing  "

    ''' <summary>
    ''' This function will mark an entry in Trace that will depict the entry to a function
    ''' </summary>
    ''' <param name="FunctionName"></param>
    ''' <remarks></remarks>
    Public Shared Sub Entering(ByVal FunctionName As String)
        HttpContext.Current.Trace.Write("  ========== ENTERING Function/Sub [ " & FunctionName & " ] ==========")
    End Sub

    ''' <summary>
    ''' This function will mark an entry in Trace that will depict the exit from a function
    ''' </summary>
    ''' <param name="FunctionName"></param>
    ''' <remarks></remarks>
    Public Shared Sub Leaving(ByVal FunctionName As String)
        HttpContext.Current.Trace.Write("  ========== LEAVING  Function/Sub [ " & FunctionName & " ] ==========")
    End Sub

    Public Shared Function getUploadFolder() As String
        Dim Folder As String
        Folder = ConfigurationManager.AppSettings("UploadFolder")

        Return Folder
    End Function

    Public Shared Function getFileNameWithoutExtension(ByVal FileName As String) As String
        Dim EXT As String

        If FileName.Length <= 0 Then Return ""

        EXT = getFileExtension(FileName)
        'HttpContext.Current.Response.Write(EXT)
        FileName = FileName.Substring(0, FileName.ToUpper.LastIndexOf(EXT))

        Return FileName
    End Function
    Public Shared Function getToken(ByVal SectionName As String, ByVal ResourceId As String) As String
        On Error Resume Next
        Dim TokenName As String
        TokenName = ""
        TokenName = "UR" & getLoggedUserID() & "SN" & SectionName & "ID" & ResourceId
        TokenName &= "HR" & Hour(Now) & "MN" & Minute(Now) & "SE" & Second(Now) & "MS" & DateTime.Now.Millisecond

        Dim IPadd As String
        IPadd = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")

        Dim ar = Split(IPadd, ".")

        TokenName &= "IPA" & ar(0)
        TokenName &= "IPB" & ar(1)
        TokenName &= "IPC" & ar(2)
        TokenName &= "IPD" & ar(3)

        TokenName &= "X"
        Return TokenName
    End Function


    Public Shared Function getFileExtension(ByVal FileName As String) As String
        Dim EXT As String

        If FileName.Length <= 0 Then Return ""


        EXT = FileName.Substring(FileName.IndexOf(".", FileName.ToString.Length - 5))

        If EXT.Substring(Len(EXT) - 1) <> "." Then
            For i As Integer = EXT.Length - 1 To 0
                If EXT.Substring(i, 1) = "." Then
                    EXT = EXT.Substring(i)
                    Exit For
                End If
            Next
        End If

        EXT = UCase(EXT.Replace(".", ""))

        Return EXT
    End Function

    Public Shared Function getFileType(ByVal FileName As String) As FileType
        Dim EXT As String
        EXT = getFileExtension(FileName)

        Select Case EXT
            Case "JPG", "JPEG", "JPE", "BMP", "GIF", "PNG", "WBMP", "GIFF", "TIFF", "TIF"
                Return FileType.Image
            Case "PDF"
                Return FileType.Pdf
            Case "DOC", "DOCX", "TXT", "RTF", "HTML", "HTM"
                Return FileType.Word
            Case "XLS", "XLSX", "CSV"
                Return FileType.Excel
            Case "PPT", "PPS"
                Return FileType.PPT
            Case "ZIP", "TAR", "RAR"
                Return FileType.Zip
            Case "WMV", "3GP", "MOV", "RM", "FLV", "AVI"
                Return FileType.Video
            Case "MP3", "WAV", "MIDI", "MP4"
                Return FileType.Audio
            Case Else
                Return FileType.Other
        End Select


    End Function

    Public Shared Function getFileIcon(ByVal FileName As String) As String
        Dim IconPath As String
        IconPath = "images/spacer.gif"

        Select Case getFileType(FileName)
            Case FileType.Audio
                IconPath = "images/Icon_Audio.jpg"
            Case FileType.Excel
                IconPath = "images/Icon_Excel.jpg"
            Case FileType.Image
                IconPath = "images/Icon_Image.jpg"
            Case FileType.Pdf
                IconPath = "images/Icon_PDF.jpg"
            Case FileType.PPT
                IconPath = "images/Icon_PPT.jpg"
            Case FileType.Video
                IconPath = "images/Icon_Video.jpg"
            Case FileType.Word
                IconPath = "images/Icon_Doc.jpg"
            Case FileType.Zip
                IconPath = "images/Icon_Rar.jpg"
            Case FileType.Other
                IconPath = "images/Icon_Other.jpg"
            Case Else
                IconPath = "images/spacer.gif"
        End Select

        Return IconPath
    End Function

    Public Shared Function MakeThubnail(ByVal fileUpload As FileUpload, Optional ByVal ImageHeight As Integer = 0, Optional ByVal ImageWidth As Integer = 0, Optional ByVal FileName As String = "", Optional ByVal IsThubnail As Boolean = False) As String
        If fileUpload.HasFile Then
            ' Find the fileUpload control
            If FileName = "" Then
                FileName = fileUpload.FileName
            End If

            Dim UploadFolderName As String = getUploadFolder()
            ' Check if the directory we want the image uploaded to actually exists or not
            If Not IO.Directory.Exists(HttpContext.Current.Server.MapPath(UploadFolderName)) Then
                ' If it doesn't then we just create it before going any further
                IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(UploadFolderName))
            End If
            ' Specify the upload directory
            Dim directory As String
            If IsThubnail Then
                directory = HttpContext.Current.Server.MapPath(UploadFolderName & "\Thumbnails" & "\")
            Else
                directory = HttpContext.Current.Server.MapPath(UploadFolderName & "\")
            End If


            ' Create a bitmap of the content of the fileUpload control in memory
            Dim originalBMP As New Bitmap(fileUpload.FileContent)

            ' Calculate the new image dimensions
            Dim origWidth As Integer = originalBMP.Width
            Dim origHeight As Integer = originalBMP.Height
            Dim sngRatio As Integer
            Dim newWidth As Integer
            Dim newHeight As Integer
            If origHeight > origWidth Then
                sngRatio = origHeight / origWidth
            Else
                sngRatio = origWidth / origHeight
            End If
            If ImageHeight <> 0 And ImageWidth <> 0 Then
                If origHeight > origWidth Then
                    newWidth = ImageHeight
                    newHeight = ImageWidth
                Else
                    newWidth = ImageWidth
                    newHeight = ImageHeight
                End If
            ElseIf ImageHeight <> 0 And ImageWidth = 0 Then
                newHeight = ImageHeight
                newWidth = newHeight / sngRatio
            ElseIf ImageHeight = 0 And ImageWidth <> 0 Then
                newWidth = ImageWidth
                newHeight = ImageWidth / sngRatio
            Else
                newWidth = origWidth
                newHeight = origHeight
            End If

            'newWidth = 100
            'newHeight = newWidth / sngRatio

            ' Create a new bitmap which will hold the previous resized bitmap
            Dim newBMP As New Bitmap(originalBMP, newWidth, newHeight)

            ' Create a graphic based on the new bitmap
            Dim oGraphics As Graphics = Graphics.FromImage(newBMP)
            ' Set the properties for the new graphic file
            oGraphics.SmoothingMode = SmoothingMode.AntiAlias
            oGraphics.InterpolationMode = InterpolationMode.Low

            ' Draw the new graphic based on the resized bitmap
            oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight)
            ' Save the new graphic file to the server
            newBMP.Save(directory & FileName)

            ' Once finished with the bitmap objects, we deallocate them.
            originalBMP.Dispose()
            newBMP.Dispose()
            oGraphics.Dispose()

            ' Write a message to inform the user all is OK
            'Label.Text = "File Name: <b style='color: red;'>" & filename & "</b><br>"
            'Label.Text += "Content Type: <b style='color: red;'>" & FileUpload.PostedFile.ContentType & "</b><br>"
            'Label.Text += "File Size: <b style='color: red;'>" & FileUpload.PostedFile.ContentLength.ToString() & "</b>"

            ' Display the image to the user
            If IsThubnail Then
                Return FileName
            Else
                Return FileName
            End If

        Else
            Return ""
        End If
        Return ""
    End Function


#End Region


#Region "  Error Logging  "

    Public Shared Function LogError(ByVal ex As Exception, Optional ByVal CustomExplanation As String = "", Optional ByVal SectionName As String = "", Optional ByVal PageName As String = "", Optional ByVal FunctionName As String = "") As String
        Dim retMessage As String
        retMessage = ex.ToString

        HttpContext.Current.Trace.Warn("## ERROR ##  ")
        HttpContext.Current.Trace.Warn("Page: " & PageName & vbNewLine & "Section: " & SectionName & vbNewLine & "Function: " & FunctionName & vbNewLine & vbNewLine & ex.ToString)

        Return retMessage
    End Function

#End Region


    Public Shared Function sendMail(ByVal Subject As String, ByVal mailBody As String, ByVal PlainText As String, ByVal ToAddress As String, ByVal CCAddress As String, Optional ByVal FromAddress As String = "alert@kestoneapps.com", Optional ByVal FromName As String = "Kestone") As Boolean
        Try
            Dim mailOB As New MailMessage

            mailOB.IsBodyHtml = True
            mailOB.From = New MailAddress(FromAddress, FromName)
            mailOB.To.Add(New MailAddress(ToAddress))
            If CCAddress.Trim <> "" Then
                mailOB.CC.Add(New MailAddress(CCAddress))
            End If

            mailOB.Subject = Subject



            'first we create the Plain Text part
            Dim plainView As AlternateView = AlternateView.CreateAlternateViewFromString(PlainText, Nothing, "text/plain")

            'then we create the Html part
            'to embed images, we need to use the prefix 'cid' in the img src value
            'the cid value will map to the Content-Id of a Linked resource.
            'thus <img src='cid:companylogo'> will map to a LinkedResource with a ContentId of 'companylogo'
            Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString(mailBody, Nothing, "text/html")


            'add the views
            mailOB.AlternateViews.Add(plainView)
            mailOB.AlternateViews.Add(htmlView)

            Dim smtpOB As New System.Net.Mail.SmtpClient
            'smtpOB.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
            'smtpOB.Host = "localhost"        
            smtpOB.Host = "kestoneapps.com"
            Dim mailAuthentication As System.Net.NetworkCredential = New System.Net.NetworkCredential("alert@kestoneapps.com", "Alert@password")
            smtpOB.Credentials = mailAuthentication
            smtpOB.Send(mailOB)
            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

    Public Shared Function sendMailForEventClander(ByVal Subject As String, ByVal mailBody As String, ByVal PlainText As String, ByVal ToAddress As String, ByVal CCAddress As String, Optional ByVal FromAddress As String = "alert@kestoneapps.com", Optional ByVal FromName As String = "Kestone") As Boolean
        Try
            Dim mailOB As New MailMessage

            mailOB.IsBodyHtml = True
            mailOB.From = New MailAddress(FromAddress, FromName)
            mailOB.To.Add(New MailAddress(ToAddress))
            If CCAddress.Trim <> "" Then
                mailOB.CC.Add(New MailAddress(CCAddress))
            End If

            mailOB.Subject = Subject



            'first we create the Plain Text part
            Dim plainView As AlternateView = AlternateView.CreateAlternateViewFromString(PlainText, Nothing, "text/plain")

            'then we create the Html part
            'to embed images, we need to use the prefix 'cid' in the img src value
            'the cid value will map to the Content-Id of a Linked resource.
            'thus <img src='cid:companylogo'> will map to a LinkedResource with a ContentId of 'companylogo'
            Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString(mailBody, Nothing, "text/html")


            'add the views
            mailOB.AlternateViews.Add(plainView)
            mailOB.AlternateViews.Add(htmlView)

            Dim smtpOB As New System.Net.Mail.SmtpClient
            'smtpOB.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
            'smtpOB.Host = "localhost"        
            smtpOB.Host = "kestoneapps.com"
            Dim mailAuthentication As System.Net.NetworkCredential = New System.Net.NetworkCredential("alert@kestoneapps.com", "Alert@password")
            smtpOB.Credentials = mailAuthentication
            smtpOB.Send(mailOB)
            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

#Region "  Data Formatting  "

    Public Shared Function formatDate(ByVal dt As String) As String
        Entering("FormatDate with parameter dt: " & dt)
        Dim retSTR As String = ""

        If IsDate(dt) Then
            'retSTR = MonthName(Month(dt), True) & " " & Day(dt) & ", " & Year(dt)
            retSTR = FormatDateTime(dt, DateFormat.LongDate) & " " & FormatDateTime(dt, DateFormat.ShortTime)
        End If
        Leaving("FormatDate: Returning " & retSTR)
        Return retSTR
    End Function

    ''' <summary>
    ''' Function to return the Current Indian Standard Time (GMT +0530)
    ''' </summary>
    ''' <returns>Current IST</returns>
    ''' <remarks>VSR 14-5-2010</remarks>
    Public Shared Function getDate() As DateTime

        Dim IST As DateTime
        Dim CaseOffset As TimeSpan = New TimeSpan(5, 30, 0)
        IST = DateTime.UtcNow.Add(CaseOffset)
        Return IST

    End Function


    Public Shared Sub getError(ByVal excep As String, ByVal ErrDesc As String, ByVal ErrNo As String, ByVal ErrModule As String, ByVal Errfunction As String)
        Dim retMessage, edesc As String
        retMessage = excep.Replace("'", "").ToString()
        edesc = ErrDesc.Replace("'", "").ToString()
        Dim sql As String = ""
        sql = ""
        sql &= "INSERT INTO [RMG_ErrorLog]"
        sql &= "           ([ErrCode]"
        sql &= "           ,[ErrMessage]"
        sql &= "           ,[ErrDescription]"
        sql &= "           ,[ErrModule]"
        sql &= "           ,[ErrFunction]"
        sql &= "           ,[InsertedOn])"
        sql &= "     VALUES"
        sql &= "           ('" & ErrNo & "'"
        sql &= "           ,'" & retMessage & "'"
        sql &= "           ,'" & edesc & "'"
        sql &= "           ,'" & ErrModule & "'"
        sql &= "           ,'" & Errfunction & "'"
        sql &= "           ,'" & getDate() & "')"
        ExecuteNonQuery(sql)
    End Sub

#End Region

End Class
