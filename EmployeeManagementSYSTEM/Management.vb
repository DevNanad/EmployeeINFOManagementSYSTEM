Imports System.Data.OleDb
Module Management

    Class Connectioncenter 'base class
        Public MyConnection As New OleDbConnection
        Public MyCommand As New OleDbCommand
        Public sdr As OleDbDataReader
        Public MySQL As String

    End Class
    'Public Function Opencon() As Boolean
    '    Connect()

    '    Try
    '        MyConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=empDB.mdb")
    '        MyConnection.Open()
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function


    Public Sub EmployeeDesign(ByVal name As String, id As Integer)
        Dim emp1 As String
        Dim emp2 As String
        Dim emp3 As String
        Dim emp4 As String
        Dim emp5 As String
        Dim topline As Integer = 0
        Dim botline As Integer = 0
        Console.Title = "Employee " & name & " with id " & id


        emp1 = "eeeeee     mm       mm     pppppp      ll           oooo       yyy    yyy     eeeeee    eeeeee"
        emp2 = "ee         mm mm mm mm     pp    p     ll         oo    oo       yy  yy       ee        ee    "
        emp3 = "eeee       mm   m   mm     pppppp      ll         oo    oo         yy         eeee      eeee  "
        emp4 = "ee         mm       mm     pp          ll         oo    oo         yy         ee        ee    "
        emp5 = "eeeeee     mm       mm     pp          llllll       oooo           yy         eeeeee    eeeeee"




        Console.ForegroundColor = ConsoleColor.DarkBlue
        Do While topline < Console.WindowWidth
            Console.Write("=")
            topline += 1
        Loop

        Console.WriteLine("")

        Console.ForegroundColor = ConsoleColor.Green


        Console.WriteLine(emp1.PadLeft((Console.WindowWidth / 2) + (emp1.Length / 2)))
        Console.WriteLine(emp2.PadLeft((Console.WindowWidth / 2) + (emp2.Length / 2)))
        Console.WriteLine(emp3.PadLeft((Console.WindowWidth / 2) + (emp3.Length / 2)))
        Console.WriteLine(emp4.PadLeft((Console.WindowWidth / 2) + (emp4.Length / 2)))
        Console.WriteLine(emp5.PadLeft((Console.WindowWidth / 2) + (emp5.Length / 2)))

        Console.WriteLine("")

        Console.ForegroundColor = ConsoleColor.DarkBlue
        Do While botline < Console.WindowWidth
            Console.Write("=")
            botline += 1
        Loop
    End Sub



    Class User 'derived class
        Inherits Connectioncenter
        Protected Friend userName As String
        Protected Friend userPassword As String
        Public id As Integer

        Public Overridable Function Dashboard(ByVal name As String, ByVal id As Integer)
            Return True
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



            Console.WriteLine(Format(Now, "dddd,d MMM yyyy"))
            usernamepromt = "Enter Username: "
            passwordpromt = "Enter Password: "
            Console.Write(usernamepromt.PadLeft((Console.WindowWidth / 2) + (usernamepromt.Length / 20)))
            Console.ForegroundColor = ConsoleColor.DarkGreen
            userName = Console.ReadLine.ToString
            Console.ResetColor()
            Console.WriteLine("")
            Console.Write(passwordpromt.PadLeft((Console.WindowWidth / 2) + (passwordpromt.Length / 20)))
            Console.ForegroundColor = ConsoleColor.Black
            userPassword = Console.ReadLine.ToString
            Console.ResetColor()
            Login()


        End Sub

        Public Function Login()
            Try
                MyConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=empDB.mdb")
                MyConnection.Open()
                MySQL = "SELECT UserID, Uname, [Upassword], isAdmin FROM [emp] WHERE Uname = @N AND [Upassword] = @P"
                MyCommand = New OleDbCommand(MySQL, MyConnection)
                MyCommand.CommandType = CommandType.Text
                MyCommand.Parameters.AddWithValue("@N", userName.ToString)
                MyCommand.Parameters.AddWithValue("@P", userPassword.ToString)
                sdr = MyCommand.ExecuteReader

                If userName = "" Or userPassword = "" Then 'kung empty string yung na pass di makakalogin
                    Console.Clear()
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("   Fields Should not be Empty!")
                    Console.ResetColor()
                    Main()
                Else

                    If sdr.Read Then
                        Dim classAdmin As New Admin
                        If sdr("IsAdmin") Then
                            id = sdr("UserID")
                            Console.Clear() 'clear lang ang console
                            Console.WriteLine("logged in")
                            classAdmin.Dashboard(userName, id) 'execute the admin interface

                        Else
                            id = sdr("UserID")
                            Console.Clear() 'clear lang ang console

                            EmployeeDesign(userName, id) 'Execute the employee interface

                        End If
                    Else
                        Console.Clear()
                        Console.ForegroundColor = ConsoleColor.Red
                        Console.WriteLine("   Invalid Username or Password!")
                        Console.ResetColor()
                        Main()

                    End If
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
    'create class employee

    Class Employee
        Inherits User


        Public Overrides Function Dashboard(name As String, id As Integer)

        End Function



    End Class
    Class Admin
        Inherits User
        Public User_ID As Integer
        Public admin, fname, lname, mname, gender, position, department, nationality, email, status, address As String
        Public age As Integer
        Public dob As Date
        Public cnumber As Integer
        Public salary As Decimal


        'CREATE ACCOUNT MULTI-ROLE
        Public Function Create(ByRef currentADMIN As String)
            Createloop("")

            While userName = "" Or userPassword = "" Or admin = "" Or department = ""
                Createloop(" Fields should not be empty!")

            End While

            Try
                MyConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=empDB.mdb")
                MyConnection.Open()
                MySQL = "SELECT [UserID],[Uname] FROM [emp] WHERE [UserID] =@I OR [Uname] =@U"
                MyCommand = New OleDbCommand(MySQL, MyConnection)
                MyCommand.CommandType = CommandType.Text
                MyCommand.Parameters.AddWithValue("@I", CInt(User_ID))
                MyCommand.Parameters.AddWithValue("@U", userName)
                sdr = MyCommand.ExecuteReader

                If sdr.Read Then
                    Console.WriteLine(" Username or Id already Exist :(")
                    Createloop("")
                Else
                    Try
                        Dim hireddate As Date = Format(Now, "ddd,d MMM yyyy")
                        MySQL = "INSERT INTO [emp] ([UserID],[Department_ID],[Uname],[Upassword],[isAdmin],[HiredDate]) " &
                        "VALUES (?,?,?,?,?,?)"
                        MyConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=empDB.mdb")
                        MyCommand = New OleDbCommand(MySQL, MyConnection)
                        MyConnection.Open()
                        MyCommand.Parameters.AddWithValue("?", CInt(User_ID))
                        MyCommand.Parameters.AddWithValue("?", department)
                        MyCommand.Parameters.AddWithValue("?", userName)
                        MyCommand.Parameters.AddWithValue("?", userPassword)
                        MyCommand.Parameters.AddWithValue("?", CBool(admin))
                        MyCommand.Parameters.AddWithValue("?", hireddate)
                        MyCommand.ExecuteNonQuery()

                        Console.Clear()
                        Console.ForegroundColor = ConsoleColor.Green
                        Console.WriteLine("  EMPLOYEE CREATED :)")
                        Console.ResetColor()
                        Dashboard(currentADMIN, 1)

                    Catch ex As Exception
                        Console.WriteLine("Error" & ex.Message)

                    End Try
                End If

            Catch ex As Exception
                Console.WriteLine(" Failed check" & ex.Message)
            End Try





            Return True
        End Function

        'LOOPCREATE FUNCTION
        Public Function Createloop(ByVal promt As String)
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(promt)
            Console.ResetColor()
            Console.WriteLine("")
            Console.WriteLine(" CREATE EMPLOYEE:")
            Console.WriteLine("")
            Console.WriteLine("")
            Console.Write("   Id: ")
            User_ID = CInt(Console.ReadLine)
            Console.Write("   Department (IT-001 | HR-002 | RD-003 | P-004 | AF-005 | P-006): ")
            department = Console.ReadLine.ToString
            Console.Write("   Username: ")
            userName = Console.ReadLine.ToString
            Console.Write("   Password: ")
            userPassword = Console.ReadLine.ToString
            Console.Write("   Admin(True/False): ")
            admin = Console.ReadLine
            Console.WriteLine("")

            Return True
        End Function




        'UPDATE ACCOUNT OF EMPLOYEE
        Public Function UpdateAccountEmployee()

            Try
                Console.WriteLine("")
                Console.WriteLine("")
                Console.WriteLine(" EDIT EMPLOYEE ACCOUNT")
                Dim empID As String
                Dim newuserName As String
                Dim newuserPassword As String

                Console.WriteLine("")
                Console.Write("   Enter employee Id: ")
                empID = Console.ReadLine

                If empID = "" Then
                    Console.WriteLine("Id should not be empty!")
                    UpdateAccountEmployee()
                Else
                    MyConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=empDB.mdb")
                    MyConnection.Open()
                    MySQL = "SELECT * FROM emp WHERE UserID =@M AND isAdmin =@F"
                    MyCommand = New OleDbCommand(MySQL, MyConnection)
                    MyCommand.CommandType = CommandType.Text
                    MyCommand.Parameters.AddWithValue("@M", empID)
                    MyCommand.Parameters.AddWithValue("@F", False)
                    sdr = MyCommand.ExecuteReader



                    If sdr.Read() Then
                        Try
                            Console.WriteLine("")
                            Console.Write("   Enter new username: ")
                            newuserName = Console.ReadLine

                            Console.Write("   Enter new password: ")
                            newuserPassword = Console.ReadLine

                            Console.WriteLine("")

                            While empID.Length = 0 Or newuserName.Length = 0 Or newuserPassword.Length = 0
                                Console.WriteLine("Field shoud not be empty!")

                                Console.Write("   Enter new username: ")
                                newuserName = Console.ReadLine

                                Console.Write("   Enter new password: ")
                                newuserPassword = Console.ReadLine

                                Console.WriteLine("")
                            End While

                            MySQL = "UPDATE [emp] SET [Uname]=?, [Upassword]=? where UserID=? and isAdmin =?"
                            MyConnection = New OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=empdb.mdb")
                            MyCommand = New OleDbCommand(MySQL, MyConnection)
                            MyConnection.Open()
                            MyCommand.Parameters.AddWithValue("?", newuserName)
                            MyCommand.Parameters.AddWithValue("?", newuserPassword)
                            MyCommand.Parameters.AddWithValue("?", empID)
                            MyCommand.Parameters.AddWithValue("?", False)
                            MyCommand.ExecuteNonQuery()

                            Console.Clear()
                            Console.WriteLine("Employee Account Updated :)")

                            Dashboard("", 0)


                        Catch ex As Exception
                            Console.WriteLine("failed" & ex.Message)
                        End Try
                    Else
                        Console.WriteLine("Enter a valid ID!")
                        UpdateAccountEmployee()
                    End If
                End If
                Return True
            Catch ex As Exception
                Console.WriteLine("Failed queeerryyyy1111" & ex.Message)
                Return False
            End Try



        End Function

        'UPDATE ACCOUNT OF ADMIN
        Public Function UpdateAccountAdmin() 'update account using sub procedure
            Try
                Console.WriteLine("")
                Console.WriteLine("")
                Console.WriteLine(" EDIT ADMIN ACCOUNT")
                Dim adminID As String
                Dim newuserName As String
                Dim newuserPassword As String

                Console.WriteLine("")
                Console.Write("   Enter Admin Id: ")
                adminID = Console.ReadLine

                If adminID = "" Then
                    Console.WriteLine("Id should not be empty!")
                    UpdateAccountAdmin()
                Else
                    MyConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=empDB.mdb")
                    MyConnection.Open()
                    MySQL = "SELECT * FROM emp WHERE UserID =@M AND isAdmin =@F"
                    MyCommand = New OleDbCommand(MySQL, MyConnection)
                    MyCommand.CommandType = CommandType.Text
                    MyCommand.Parameters.AddWithValue("@M", adminID)
                    MyCommand.Parameters.AddWithValue("@F", True)
                    sdr = MyCommand.ExecuteReader



                    If sdr.Read() Then
                        Try
                            Console.WriteLine("")
                            Console.Write("   Enter new username: ")
                            newuserName = Console.ReadLine

                            Console.Write("   Enter new password: ")
                            newuserPassword = Console.ReadLine

                            Console.WriteLine("")

                            While adminID.Length = 0 Or newuserName.Length = 0 Or newuserPassword.Length = 0
                                Console.WriteLine("Field shoud not be empty!")

                                Console.Write("   Enter new username: ")
                                newuserName = Console.ReadLine

                                Console.Write("   Enter new password: ")
                                newuserPassword = Console.ReadLine

                                Console.WriteLine("")
                            End While

                            MySQL = "UPDATE [emp] SET [Uname]=?, [Upassword]=? WHERE UserID=? and isAdmin =?"
                            MyConnection = New OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=empdb.mdb")
                            MyCommand = New OleDbCommand(MySQL, MyConnection)
                            MyConnection.Open()
                            MyCommand.Parameters.AddWithValue("?", newuserName)
                            MyCommand.Parameters.AddWithValue("?", newuserPassword)
                            MyCommand.Parameters.AddWithValue("?", adminID)
                            MyCommand.Parameters.AddWithValue("?", True)
                            MyCommand.ExecuteNonQuery()

                            Console.Clear()
                            Console.WriteLine("Admin Account Updated :)")

                            Dashboard("", 0)


                        Catch ex As Exception
                            Console.WriteLine("failed" & ex.Message)
                        End Try
                    Else
                        Console.WriteLine("Enter a valid ID!")
                        UpdateAccountAdmin()
                    End If
                End If
                Return True
            Catch ex As Exception
                Console.WriteLine("Failed queeerryyyy2222" & ex.Message)
                Return False
            End Try
        End Function

        'DELETE ACCOUNT FOR EVERYONE
        Public Function DeleteAccount()
            Try

                Console.WriteLine("")
                Console.WriteLine("")
                Console.WriteLine(" DELETE ACCOUNT")
                Dim UID As String


                Console.WriteLine("")
                Console.Write("   Enter Admin Id: ")
                UID = Console.ReadLine

                If UID = "" Then
                    Console.WriteLine("  Id should not be empty!")
                    DeleteAccount()
                Else
                    Dim delname As String
                    MyConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=empDB.mdb")
                    MyConnection.Open()
                    MySQL = "SELECT [UserID],[Uname] FROM [emp] WHERE UserID =@M"
                    MyCommand = New OleDbCommand(MySQL, MyConnection)
                    MyCommand.CommandType = CommandType.Text
                    MyCommand.Parameters.AddWithValue("@M", UID)
                    sdr = MyCommand.ExecuteReader




                    If sdr.Read() Then
                        Try
                            Dim torf, choose, warning As String
                            Console.ForegroundColor = ConsoleColor.DarkGreen
                            delname = sdr("Uname").ToString
                            Console.ResetColor()
                            warning = "Are you sure to Delete user " & delname & " ?"
                            choose = "[Y] Yes      [N] No"


                            Console.WriteLine(warning.PadLeft((Console.WindowWidth / 2) + (warning.Length / 2)))
                            Console.WriteLine("")
                            Console.ForegroundColor = ConsoleColor.DarkYellow
                            Console.WriteLine(choose.PadLeft((Console.WindowWidth / 2) + (choose.Length / 2)))
                            Console.ResetColor()
                            Console.ForegroundColor = ConsoleColor.DarkCyan
                            Console.Write("     I Choose : ")
                            Console.ResetColor()
                            torf = Console.ReadKey().Key

                            Do While torf <> ConsoleKey.Y And torf <> ConsoleKey.N
                                Console.WriteLine("")
                                Console.ForegroundColor = ConsoleColor.DarkCyan
                                Console.Write("     I Choose : ")
                                Console.ResetColor()
                                torf = Console.ReadKey().Key



                            Loop

                            If torf = ConsoleKey.Y Then
                                MySQL = "DELETE FROM [emp] WHERE [UserID] =?"
                                MyConnection = New OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=empdb.mdb")
                                MyCommand = New OleDbCommand(MySQL, MyConnection)
                                MyConnection.Open()
                                MyCommand.Parameters.AddWithValue("?", CInt(UID))
                                MyCommand.ExecuteNonQuery()
                                Console.Clear()
                                Console.WriteLine("  ACCOUNT DELETED :)")
                                Dashboard("", 0)

                            ElseIf torf = ConsoleKey.N Then
                                Console.Clear()
                                Console.WriteLine("  Operation Canceled :)")
                                Dashboard("", 0)

                            End If

                        Catch ex As Exception
                            Console.WriteLine(" Failed" & ex.Message)
                        End Try
                    Else
                        Console.WriteLine("  Enter a valid ID!")
                        DeleteAccount()
                    End If
                End If

            Catch ex As Exception
                Console.WriteLine(" Error here in delete trycatch " & ex.Message)
            End Try
            Return True
        End Function

        'SEARCH EMPLOYEE    
        Public Function SearchEmployeeInfo()

        End Function

        'ADMIN DASHBOARD (OVERRIDES | POLYMORPHISM)
        Public Overrides Function Dashboard(ByVal name As String, ByVal id As Integer) As Object
            Dim emp1 As String
            Dim emp2 As String
            Dim emp3 As String
            Dim emp4 As String
            Dim emp5 As String
            Dim choice As String
            Dim choice1 As String
            Dim inputval As String
            Dim slct As String
            Dim line1, line2, topline As String


            Console.Title = "ADMIN " & name & " with an Id " & id

            line1 = "                      ="
            line2 = "                         ="
            topline = "||========================================================================================================||"
            emp1 = "  aaaa       ddddddd       mm       mm     ii     nn    nn"
            emp2 = "aa    aa     dd     dd     mm mm mm mm     ii     nnnn  nn"
            emp3 = "aaaaaaaa     dd      dd    mm   m   mm     ii     nn  nnnn"
            emp4 = "aa    aa     dd     dd     mm       mm     ii     nn   nnn"
            emp5 = "aa    aa     ddddddd       mm       mm     ii     nn    nn"

            slct = "</ SELECT OPTION \>"

            Console.WriteLine("")

            Console.ForegroundColor = ConsoleColor.Gray
            Console.WriteLine(topline.PadLeft((Console.WindowWidth / 2) + (topline.Length / 2)))
            Console.WriteLine(line1 & "                                                                                 " & line2)
            Console.Write(line1)
            Console.ForegroundColor = ConsoleColor.DarkCyan
            Console.Write(emp1.PadLeft((Console.WindowWidth / 2) + (emp1.Length / 10)))
            Console.ResetColor()
            Console.WriteLine(line2)
            Console.Write(line1)
            Console.ForegroundColor = ConsoleColor.DarkCyan
            Console.Write(emp2.PadLeft((Console.WindowWidth / 2) + (emp2.Length / 10)))
            Console.ResetColor()
            Console.WriteLine(line2)
            Console.Write(line1)
            Console.ForegroundColor = ConsoleColor.DarkCyan
            Console.Write(emp3.PadLeft((Console.WindowWidth / 2) + (emp3.Length / 10)))
            Console.ResetColor()
            Console.WriteLine(line2)
            Console.Write(line1)
            Console.ForegroundColor = ConsoleColor.DarkCyan
            Console.Write(emp4.PadLeft((Console.WindowWidth / 2) + (emp4.Length / 10)))
            Console.ResetColor()
            Console.WriteLine(line2)
            Console.Write(line1)
            Console.ForegroundColor = ConsoleColor.DarkCyan
            Console.Write(emp5.PadLeft((Console.WindowWidth / 2) + (emp5.Length / 10)))
            Console.ResetColor()
            Console.WriteLine(line2)
            Console.WriteLine(line1 & "                                                                                 " & line2)
            Console.WriteLine(topline.PadLeft((Console.WindowWidth / 2) + (topline.Length / 2)))
            Console.WriteLine("")

            Console.ResetColor()
            Console.WriteLine("")
            Console.ForegroundColor = ConsoleColor.DarkGray
            Console.WriteLine("                                                                                                                        [0] LOGOUT ")
            Console.ResetColor()
            Console.ForegroundColor = ConsoleColor.Cyan
            Console.WriteLine("")
            Console.WriteLine("")
            Console.WriteLine(slct.PadLeft((Console.WindowWidth / 2) + (slct.Length / 2)))
            Console.WriteLine("")
            Console.ResetColor()


            choice = " [1] CREATE EMPLOYEE ACCOUNT      [3] EDIT EMPLOYEE    "
            choice1 = " [2] SEARCH EMPLOYEE              [4] DELETE EMPLOYEE  "


            Console.ForegroundColor = ConsoleColor.DarkYellow
            Console.WriteLine(choice.PadLeft((Console.WindowWidth / 2) + (choice.Length / 2)))
            Console.WriteLine("")
            Console.WriteLine(choice1.PadLeft((Console.WindowWidth / 2) + (choice1.Length / 2)))
            Console.ResetColor()
            Console.WriteLine("")
            Console.WriteLine("")
            Console.WriteLine("")
            Console.ForegroundColor = ConsoleColor.Cyan
            Console.Write("  SELECT CHOICE: ")
            Console.ResetColor()
            Console.ForegroundColor = ConsoleColor.White
            inputval = CStr(Console.ReadLine())
            Console.ResetColor()



            While inputval.Length = 0
                Console.ForegroundColor = ConsoleColor.DarkRed
                Console.WriteLine(" Please enter a value!")
                Console.ResetColor()
                Console.WriteLine("")
                Console.WriteLine("")
                Console.WriteLine("")
                Console.ForegroundColor = ConsoleColor.Cyan
                Console.Write("  SELECT CHOICE: ")
                Console.ResetColor()
                Console.ForegroundColor = ConsoleColor.White
                inputval = CStr(Console.ReadLine())
                Console.ResetColor()

            End While

            Do While inputval <> "1" And inputval <> "2" And inputval <> "3" And inputval <> "4" And inputval <> "0"
                Console.ForegroundColor = ConsoleColor.DarkRed
                Console.WriteLine(" Input must be on the list or not empty")
                Console.ResetColor()
                Console.WriteLine("")
                Console.WriteLine("")
                Console.WriteLine("")
                Console.ForegroundColor = ConsoleColor.Cyan
                Console.Write("  SELECT CHOICE: ")
                Console.ResetColor()
                Console.ForegroundColor = ConsoleColor.White
                inputval = CStr(Console.ReadLine())
                Console.ResetColor()
            Loop


            Select Case inputval
                Case "1"
                    Dim classAdmin As New Admin
                    classAdmin.Create(name)
                Case "2"
                    Console.WriteLine(" SEARCH EMPLOYEE")

                Case "3"

                    Dim input As String
                    Dim opt As String

                    Console.WriteLine("")
                    Console.WriteLine("   SELECT OPTION:")
                    opt = "[1] EDIT EMPLOYEE      [2] EDIT ADMIN"

                    Console.ForegroundColor = ConsoleColor.DarkYellow
                    Console.WriteLine(opt.PadLeft((Console.WindowWidth / 2) + (opt.Length / 2)))
                    Console.ResetColor()
                    Console.WriteLine("")
                    Console.WriteLine("")


                    Console.Write(" CHOOSE OPTION: ")
                    input = CStr(Console.ReadLine())


                    While input.Length = 0
                        Console.ForegroundColor = ConsoleColor.DarkRed
                        Console.WriteLine(" Please enter a value!")
                        Console.ResetColor()
                        Console.WriteLine("")
                        Console.WriteLine("")
                        Console.ForegroundColor = ConsoleColor.Cyan
                        Console.Write("  CHOOSE OPTION: ")
                        Console.ResetColor()
                        Console.ForegroundColor = ConsoleColor.White
                        input = CStr(Console.ReadLine())
                        Console.ResetColor()
                    End While

                    Do While input <> "1" And input <> "2"
                        Console.ForegroundColor = ConsoleColor.DarkRed
                        Console.WriteLine(" Input must be on the list or not empty")
                        Console.ResetColor()
                        Console.WriteLine("")
                        Console.WriteLine("")
                        Console.WriteLine("")
                        Console.ForegroundColor = ConsoleColor.Cyan
                        Console.Write("  CHOOSE OPTION: ")
                        Console.ResetColor()
                        Console.ForegroundColor = ConsoleColor.White
                        input = CStr(Console.ReadLine())
                        Console.ResetColor()
                    Loop

                    If input = "1" Then
                        UpdateAccountEmployee()
                    ElseIf input = "2" Then
                        Dim classAdmin As New Admin
                        UpdateAccountAdmin()
                    End If




                Case "4"
                    Console.WriteLine(" DELETE EMPLOYEE")

                    DeleteAccount()

                Case "0"
                    Console.Clear()
                    Dim userclass As New User
                    userclass.Logindesign()


            End Select
            Return True

        End Function

    End Class

    Sub Main()
        Dim userclass As New User
        Dim width, height As Integer

        width = 151
        height = 38
        Console.SetWindowSize(width, height)
        userclass.Logindesign()


        'Console.ResetColor()

        'If Opencon() = True Then
        '    Console.WriteLine("connected!")
        '    Opencon()
        'Else
        '    Console.WriteLine("Failed!!!!")

        'End If


    End Sub

End Module
