Imports System.Data.OleDb
Public Class User
    Public MyConnection As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=empDB.mdb")
    Public MySQL As String
    Public MyCommand As New OleDbCommand
    Public sdr As OleDbDataReader
    Private userName As String
    Private userPassword As String



    Public Overridable Function Dashboard()
        Return Nothing
    End Function

    Public Sub Logindesign()
        Dim log1 As String
        Dim log2 As String
        Dim log3 As String
        Dim log4 As String
        Dim log5 As String
        Dim usernamepromt, passwordpromt As String

        Console.Title = "Login"

        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("")


        log1 = "ll           oooo        gggggg      ii     nn    nn"
        log2 = "ll         oo    oo     gg           ii     nnnn  nn"
        log3 = "ll         oo    oo     gg  gggg     ii     nn  nnnn"
        log4 = "ll         oo    oo     gg    gg     ii     nn   nnn"
        log5 = "llllll       oooo        gggggg      ii     nn    nn"

        Console.WriteLine("")

        Console.ForegroundColor = ConsoleColor.Gray


        Console.WriteLine(log1.PadLeft((Console.WindowWidth / 2) + (log1.Length / 2)))
        Console.WriteLine(log2.PadLeft((Console.WindowWidth / 2) + (log2.Length / 2)))
        Console.WriteLine(log3.PadLeft((Console.WindowWidth / 2) + (log3.Length / 2)))
        Console.WriteLine(log4.PadLeft((Console.WindowWidth / 2) + (log4.Length / 2)))
        Console.WriteLine(log5.PadLeft((Console.WindowWidth / 2) + (log5.Length / 2)))

        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("")


        Console.SetCursorPosition(125, 2)
        Console.WriteLine(Format(Now, "dddd,d MMM yyyy"))
        usernamepromt = "Enter Username: "
        passwordpromt = "Enter Password: "

        Console.SetCursorPosition(60, 17)
        Console.Write(usernamepromt)
        Console.ForegroundColor = ConsoleColor.DarkGreen
        userName = Console.ReadLine.ToString
        Console.ResetColor()
        Console.WriteLine("")
        Console.SetCursorPosition(60, 19)
        Console.Write(passwordpromt)
        Console.ForegroundColor = ConsoleColor.Black
        userPassword = Console.ReadLine.ToString
        Console.ResetColor()
        Login()


    End Sub



    Public Function Login()

        If userName.Length = 0 Then
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("     Username Should not be empty!")
            Console.ResetColor()
            Logindesign()
        ElseIf userPassword.Length = 0 Then
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("     Password Should not be Empty!")
            Console.ResetColor()
            Logindesign()
        End If

        Try
            MyConnection.Open()
            MySQL = "SELECT UserID, Uname, [Upassword], isAdmin,Fname,Lname FROM [emp] WHERE Uname like @N AND [Upassword] like @P"
            MyCommand = New OleDbCommand(MySQL, MyConnection)
            MyCommand.CommandType = CommandType.Text
            MyCommand.Parameters.AddWithValue("@N", Trim(userName))
            MyCommand.Parameters.AddWithValue("@P", Trim(userPassword))
            sdr = MyCommand.ExecuteReader




            If sdr.Read Then
                Dim realU As String = sdr("Uname")
                Dim realP As String = CStr(sdr("Upassword"))
                Dim id As Integer

                If realU Like Trim(userName) And realP Like Trim(userPassword).ToString Then
                    If sdr("IsAdmin") Then

                        Console.Clear() 'clear lang ang console
                        Dim classAdmin As New Admin
                        id = sdr("UserID")
                        classAdmin.CurrentAdmin = sdr("Fname").ToString & " " & sdr("Lname")
                        classAdmin.CurrentAdminID = id
                        MyConnection.Close()
                        classAdmin.Dashboard() 'execute the admin interface

                    Else

                        Console.Clear() 'clear lang ang console
                        Dim classEmployee As New Employee
                        id = sdr("UserID")
                        Console.WriteLine("     Logged In :)")
                        classEmployee.CurrentEmployee = userName
                        classEmployee.currEmployeeID = id
                        MyConnection.Close()
                        classEmployee.Dashboard() 'Execute the employee interface

                    End If
                Else
                    Console.Clear()
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("     Invalid Username or Password!")
                    Console.ResetColor()
                    MyConnection.Close()
                    Logindesign()
                End If



            Else
                Console.Clear()
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("     Invalid Username or Password!")
                Console.ResetColor()
                MyConnection.Close()
                Logindesign()

            End If
            Return True
        Catch ex As Exception
            Console.ForegroundColor = ConsoleColor.DarkYellow
            Console.WriteLine("  Query Failed :(" & ex.Message)
            Console.ResetColor()
            Return False

        End Try
    End Function

End Class
