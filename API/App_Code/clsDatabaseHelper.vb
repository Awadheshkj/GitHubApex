Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data



Public Class clsDatabaseHelper
    Public Shared connectionstring As String
    Enum _DataType
        _Default
        Alpha
        Numeric
        AlphaNumeric
        DateTime
    End Enum

    Shared Sub New()

        If HttpContext.Current.Request.IsLocal Then
            connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings("localdt").ConnectionString
        Else
            connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings("onlinedt").ConnectionString
        End If

    End Sub

    Public Shared Function getConnectionStr() As String
        Return connectionstring
    End Function

    ''' <summary>
    ''' This function will execute insert/update/delete queries and will return number of rows affected
    ''' </summary>
    ''' <param name="sql">SQL Statement to be executed</param>
    ''' <returns>It returns the number of rows affected</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteNonQuery(ByVal sql As String) As Integer
        Dim con As New SqlConnection(connectionstring)
        con.Open()
        Try
            Dim cmd As New SqlCommand(sql, con)
            cmd.CommandTimeout = 440

            HttpContext.Current.Trace.Warn("Function ExecuteNonQuery - Executing Query: " & vbNewLine & sql)

            Dim retval As Integer = cmd.ExecuteNonQuery()
            con.Dispose()
            Return retval
        Catch ex As Exception
            Throw ex
        Finally
            If con.State = ConnectionState.Open Then
                con.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' This function will fill a dataset based on the SQL Query provided
    ''' </summary>
    ''' <param name="sql">SQL Query to be executed</param>
    ''' <returns>It will return the populated dataset</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteDataSet(ByVal sql As String) As DataSet

        If sql = "" Then Return Nothing

        Try
            Dim DataSetObject As New DataSet

            HttpContext.Current.Trace.Warn("Function ExecuteDataSet - Executing Query: " & vbNewLine & sql)
            Dim da As New SqlDataAdapter(sql, connectionstring)
            da.Fill(DataSetObject)
            Return DataSetObject

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ExecuteSingleResult(ByVal query As String, Optional ByVal DataType As _DataType = _DataType._Default) As String
        Dim Result As String
        Dim DR As SqlDataReader
        DR = Nothing
        HttpContext.Current.Trace.Warn("Function: [ExecuteSingleResult]")
        ExecuteReader(query, DR)
        Result = ""
        Try

            If DR.Read() Then
                If Not IsDBNull(DR(0)) Then
                    Result = DR(0)
                End If
            End If

            DR.Close()

            Select Case DataType
                Case _DataType.Alpha, _DataType.AlphaNumeric

                Case _DataType.Numeric
                    If Not IsNumeric(Result) Then
                        Result = -1
                    End If
                Case _DataType.DateTime
                    If (Not IsDate(Result)) Then
                        Result = "1/1/1900"
                    End If
                Case Else
            End Select


            HttpContext.Current.Trace.Write("Returning Result: " & Result & " from ExecuteSingleResult")

        Catch ex As Exception
            HttpContext.Current.Trace.Warn("Error occurred in function ExecuteSingleResult: " & ex.ToString)
            DR = Nothing
        End Try
        Return Result
    End Function

    Public Shared Function getConnection() As SqlConnection
        Dim con As New SqlConnection(connectionstring)
        Return con
    End Function
    'Public Shared Function getOledbConnection() As OleDbConnection
    '    Dim constrOledb As String = "Provider=SQLOLEDB;Server=192.168.1.59;Database=MSROSE;User ID=sa;Password=$webteam08"
    '    Dim con As New OleDbConnection(constrOledb)
    '    con.Open()
    '    Return con
    'End Function

    ''' <summary>
    ''' This function accepts a Query and Datareader By Reference
    ''' </summary>
    ''' <param name="query">Query to be Executed</param>
    ''' <param name="DataReaderObject">Datareader Object (by reference)</param>
    ''' <remarks>This function does not returns anything</remarks>
    Public Shared Sub ExecuteReader(ByVal query As String, ByRef DataReaderObject As SqlDataReader)

        If query.Trim = "" Then
            DataReaderObject = Nothing
            Return
        End If

        Dim cnn As New SqlConnection(connectionstring)
        Dim cmd As New SqlCommand(query, cnn)
        cmd.CommandTimeout = 440
        cnn.Open()
        HttpContext.Current.Trace.Warn("Function: [ExecuteReader]")
        HttpContext.Current.Trace.Warn("Executing Query" & vbNewLine & query)
        DataReaderObject = cmd.ExecuteReader(CommandBehavior.CloseConnection)

    End Sub
Public Shared Function ExecuteProc(ByVal sql As String) As Integer
        Dim con As New SqlConnection(connectionstring)
        con.Open()
        Try
            Dim cmd As New SqlCommand(sql, con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandTimeout = 440

            HttpContext.Current.Trace.Warn("Function ExecuteNonQuery - Executing Query: " & vbNewLine & sql)

            Dim retval As Integer = cmd.ExecuteNonQuery()
            con.Dispose()
            Return retval
        Catch ex As Exception
            Throw ex
        Finally
            If con.State = ConnectionState.Open Then
                con.Dispose()
            End If
        End Try
    End Function
Public Shared Function ExecuteStoredProcedure(ByVal StoredName As String, ByVal arrayparam As SqlParameter()) As Integer
        Dim con As New SqlConnection(connectionstring)
        con.Open()
        Try
            HttpContext.Current.Trace.Write("Function: [ExecuteNonQuery]")
            Dim mDataCom As SqlCommand
            ' = new SqlCommand();
            mDataCom = New SqlCommand()
            mDataCom.Connection = con
            mDataCom.CommandType = CommandType.StoredProcedure
            mDataCom.CommandText = StoredName
            'If arrayparam > 0 Then
            mDataCom.Parameters.Clear()
            For Each param As SqlParameter In arrayparam
                mDataCom.Parameters.Add(param)
            Next
            mDataCom.CommandTimeout = 2000
            Dim Result As Integer = mDataCom.ExecuteNonQuery()
            Return Result
        Catch ex As Exception
            Throw ex
        Finally
            If con.State = ConnectionState.Open Then
                con.Dispose()
            End If
        End Try
    End Function

End Class


