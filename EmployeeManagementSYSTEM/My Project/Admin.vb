Imports System.Data.OleDb
Public Class Admin : Inherits User
    Private newid, newuser, newpass, admin, department As String
    Private curradminid As Integer
    Private curradmin As String


    'Property to get current admin
    Public Property CurrentAdmin As String
        Get
            Return curradmin
        End Get
        Set(value As String)
            curradmin = value
        End Set
    End Property

    'Property to get current admin ID
    Public Property CurrentAdminID As Integer
        Get
            Return curradminid
        End Get
        Set(value As Integer)
            curradminid = value
        End Set
    End Property

    'Return to dashboard function
    Private Function Returndashboard(ByVal input As String)
        If input = "9" Then
            Console.Clear()
            Dashboard()
        End If
        Return True
    End Function

    'CREATE ACCOUNT MULTI-ROLE
    Private Function Create()


        Console.Write("                  Id: ")
        newid = Console.ReadLine
        Returndashboard(newid)
        Console.Write("                  Department (IT-001 | HR-002 | RD-003 | P-004 | AF-005 | P-006): ")
        department = Console.ReadLine.ToString
        Returndashboard(department)
        Console.Write("                  Username: ")
        newuser = Console.ReadLine.ToString
        Returndashboard(newuser)
        Console.Write("                  Password: ")
        newpass = Console.ReadLine.ToString
        Returndashboard(newpass)
        Console.Write("                  Admin(True/False): ")
        admin = Console.ReadLine
        Returndashboard(admin)
        Console.WriteLine("")

        While newuser = "" Or newpass = "" Or admin = "" Or department = "" Or newid = ""
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("               Fields should not be empty!")
            Console.ResetColor()
            Create()

        End While



        Try
            MyConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=empDB.mdb")
            MyConnection.Open()
            MySQL = "SELECT [UserID],[Uname] FROM [emp] WHERE [UserID] =@I OR [Uname] =@U"
            MyCommand = New OleDbCommand(MySQL, MyConnection)
            MyCommand.CommandType = CommandType.Text
            MyCommand.Parameters.AddWithValue("@I", CInt(newid))
            MyCommand.Parameters.AddWithValue("@U", newuser)
            sdr = MyCommand.ExecuteReader

            If sdr.Read Then

                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("               Username or Id already Exist :(")
                Console.ResetColor()
                Create()
            Else
                Try
                    Dim hireddate As Date = Format(Now, "ddd,d MMM yyyy")
                    MySQL = "INSERT INTO [emp] ([UserID],[Department_ID],[Uname],[Upassword],[isAdmin],[HiredDate]) " &
                    "VALUES (?,?,?,?,?,?)"
                    MyConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=empDB.mdb")
                    MyCommand = New OleDbCommand(MySQL, MyConnection)
                    MyConnection.Open()
                    MyCommand.Parameters.AddWithValue("?", CInt(newid))
                    MyCommand.Parameters.AddWithValue("?", department)
                    MyCommand.Parameters.AddWithValue("?", newuser)
                    MyCommand.Parameters.AddWithValue("?", newpass)
                    MyCommand.Parameters.AddWithValue("?", CBool(admin))
                    MyCommand.Parameters.AddWithValue("?", hireddate)
                    MyCommand.ExecuteNonQuery()

                    MyConnection.Close()

                    Console.Clear()
                    Console.ForegroundColor = ConsoleColor.Green
                    Console.WriteLine("     EMPLOYEE CREATED :)")
                    Console.ResetColor()
                    Dashboard()

                Catch ex As Exception
                    Console.WriteLine("Error" & ex.Message)

                End Try
            End If

        Catch ex As Exception
            Console.WriteLine(" Failed check" & ex.Message)
        End Try

        Return True
    End Function

    'UPDATE INTERFACE
    Private Sub EditInterface()


        Dim input As String



        Console.Write("                    CHOOSE OPTION: ")
        input = Console.ReadLine()
        If input = "9" Then
            Console.Clear()
            Dashboard()
        End If

        While input = ""
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("                       Please enter a value!")
            Console.ResetColor()
            EditInterface()
        End While

        Do While input <> "1" And input <> "2" And input <> "9"
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("                       Input must be on the list or not empty")
            Console.ResetColor()
            EditInterface()
        Loop


        If input = 1 Then
            Console.WriteLine("")
            Console.WriteLine("")
            Console.ForegroundColor = ConsoleColor.DarkCyan
            Console.WriteLine("               EDIT EMPLOYEE ACCOUNT")
            Console.ResetColor()
            Console.WriteLine("")
            UpdateAccountEmployee()
        ElseIf input = 2 Then
            UpdateAccountAdmin()
            'LOGOUT FUNCTION
            'ElseIf input = 0 Then
            '    Console.Clear()
            '    Dim userclass As New User
            '    userclass.Logindesign()
        End If

    End Sub

    'EDIT ACCOUNT OF EMPLOYEE
    Private Function UpdateAccountEmployee()

        Try

            Dim empID As String
            Dim newuserName As String
            Dim newuserPassword As String


            Console.Write("                    Enter Employee ID: ")
            empID = Console.ReadLine
            Returndashboard(empID)

            If empID = "" Then
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("                       Id should not be Empty!")
                Console.ResetColor()
                UpdateAccountEmployee()
            Else
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
                        Console.Write("                    Enter new username: ")
                        newuserName = Console.ReadLine
                        Returndashboard(newuserName)

                        Console.Write("                    Enter new password: ")
                        newuserPassword = Console.ReadLine
                        Returndashboard(newuserPassword)
                        Console.WriteLine("")

                        Do While empID.Length = 0 Or newuserName.Length = 0 Or newuserPassword.Length = 0
                            Console.ForegroundColor = ConsoleColor.Red
                            Console.WriteLine("                       Field shoud not be Empty!")
                            Console.ResetColor()

                            Console.Write("                    Enter new username: ")
                            newuserName = Console.ReadLine
                            Returndashboard(newuserName)

                            Console.Write("                    Enter new password: ")
                            newuserPassword = Console.ReadLine
                            Returndashboard(newuserPassword)

                            Console.WriteLine("")
                        Loop

                        MySQL = "UPDATE [emp] SET [Uname]=?, [Upassword]=? where UserID=? and isAdmin =?"
                        MyConnection = New OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=empdb.mdb")
                        MyCommand = New OleDbCommand(MySQL, MyConnection)
                        MyConnection.Open()
                        MyCommand.Parameters.AddWithValue("?", newuserName)
                        MyCommand.Parameters.AddWithValue("?", newuserPassword)
                        MyCommand.Parameters.AddWithValue("?", empID)
                        MyCommand.Parameters.AddWithValue("?", False)
                        MyCommand.ExecuteNonQuery()

                        MyConnection.Close()
                        Console.Clear()
                        Console.WriteLine("     Employee Account Updated :)")

                        Dashboard()


                    Catch ex As Exception
                        Console.WriteLine("Failed" & ex.Message)
                    End Try
                Else
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("                       Enter a valid ID!")
                    Console.ResetColor()
                    MyConnection.Close()
                    UpdateAccountEmployee()
                End If
            End If
            Return True
        Catch ex As Exception
            Console.WriteLine("Failed queeerryyyy1111" & ex.Message)
            Return False
        End Try



    End Function

    'EDIT ACCOUNT OF ADMIN
    Private Function UpdateAccountAdmin()
        Try

            Dim empID As String
            Dim newuserName As String
            Dim newuserPassword As String


            Console.Write("                    Enter Admin ID: ")
            empID = Console.ReadLine
            Returndashboard(empID)

            If empID = "" Then
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("                       Id should not be Empty!")
                Console.ResetColor()
                UpdateAccountEmployee()
            Else
                MyConnection.Open()
                MySQL = "SELECT * FROM emp WHERE UserID =@M AND isAdmin =@F"
                MyCommand = New OleDbCommand(MySQL, MyConnection)
                MyCommand.CommandType = CommandType.Text
                MyCommand.Parameters.AddWithValue("@M", empID)
                MyCommand.Parameters.AddWithValue("@F", True)
                sdr = MyCommand.ExecuteReader



                If sdr.Read() Then
                    Try
                        Console.WriteLine("")
                        Console.Write("                    Enter new username: ")
                        newuserName = Console.ReadLine
                        Returndashboard(newuserName)

                        Console.Write("                    Enter new password: ")
                        newuserPassword = Console.ReadLine
                        Returndashboard(newuserPassword)
                        Console.WriteLine("")

                        Do While empID.Length = 0 Or newuserName.Length = 0 Or newuserPassword.Length = 0
                            Console.ForegroundColor = ConsoleColor.Red
                            Console.WriteLine("                       Field shoud not be Empty!")
                            Console.ResetColor()

                            Console.Write("                    Enter new username: ")
                            newuserName = Console.ReadLine
                            Returndashboard(newuserName)

                            Console.Write("                    Enter new password: ")
                            newuserPassword = Console.ReadLine
                            Returndashboard(newuserPassword)

                            Console.WriteLine("")
                        Loop

                        MySQL = "UPDATE [emp] SET [Uname]=?, [Upassword]=? where UserID=? and isAdmin =?"
                        MyConnection = New OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=empdb.mdb")
                        MyCommand = New OleDbCommand(MySQL, MyConnection)
                        MyConnection.Open()
                        MyCommand.Parameters.AddWithValue("?", newuserName)
                        MyCommand.Parameters.AddWithValue("?", newuserPassword)
                        MyCommand.Parameters.AddWithValue("?", empID)
                        MyCommand.Parameters.AddWithValue("?", True)
                        MyCommand.ExecuteNonQuery()

                        MyConnection.Close()
                        Console.Clear()
                        Console.WriteLine("     Admin Account Updated :)")

                        Dashboard()


                    Catch ex As Exception
                        Console.WriteLine("Failed" & ex.Message)
                    End Try
                Else
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("                       Enter a valid ID!")
                    Console.ResetColor()
                    MyConnection.Close()
                    UpdateAccountEmployee()
                End If
            End If
            Return True
        Catch ex As Exception
            Console.WriteLine("Failed queeerryyyy1111" & ex.Message)
            Return False
        End Try

    End Function

    'DELETE ACCOUNT FOR EVERYONE
    Private Function DeleteAccount()
        Try

            Dim UID As String

            Console.Write("                    Enter Admin Id: ")
            UID = Console.ReadLine()
            Returndashboard(UID)

            If UID = "" Then
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("                       Id should not be empty!")
                Console.ResetColor()
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
                        Console.Write("                    I Choose : ")
                        Console.ResetColor()
                        torf = Console.ReadKey().Key

                        Do While torf <> ConsoleKey.Y And torf <> ConsoleKey.N
                            Console.WriteLine("")
                            Console.ForegroundColor = ConsoleColor.DarkCyan
                            Console.Write("                    I Choose : ")
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
                            Console.WriteLine("     ACCOUNT DELETED :)")
                            Dashboard()

                        ElseIf torf = ConsoleKey.N Then
                            Console.Clear()
                            Console.WriteLine("     Operation Canceled :)")
                            Dashboard()

                        End If

                    Catch ex As Exception
                        Console.WriteLine(" Failed" & ex.Message)
                    End Try
                Else
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("                       Enter a valid ID!")
                    Console.ResetColor()
                    DeleteAccount()
                End If
            End If

        Catch ex As Exception
            Console.WriteLine(" Error here in delete trycatch " & ex.Message)
        End Try
        Return True
    End Function


    'SEARCH BY NAME AND ID
    Private Function SearchFunc()

        Dim ask, search As String



        ask = "PUT ID OR NAME"
        Console.WriteLine(ask.PadLeft((Console.WindowWidth / 2) + (ask.Length / 2)))

        Console.Write("                    Search: ")
        search = Console.ReadLine()


        If search = "" Then
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("                       Field should not be empty")
            Console.ResetColor()
            SearchFunc()
        Else
            Try
                MyConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=empDB.mdb")
                MyConnection.Open()
                MySQL = "SELECT * FROM [emp] WHERE [UserID] =@N"
                MyCommand = New OleDbCommand(MySQL, MyConnection)
                MyCommand.CommandType = CommandType.Text
                MyCommand.Parameters.AddWithValue("@N", search)
                sdr = MyCommand.ExecuteReader

                If sdr.HasRows Then

                    While sdr.Read
                        Console.WriteLine("")
                        Console.WriteLine("")
                        Console.ForegroundColor = ConsoleColor.DarkYellow
                        Console.Write("                   Full Name: ")
                        Console.ResetColor()
                        Console.WriteLine("{0}  {1}  {2}", sdr("Fname").ToString, sdr("Mname").ToString, sdr("Lname").ToString)
                        Console.WriteLine("")

                        Console.ForegroundColor = ConsoleColor.DarkYellow
                        Console.Write("                   Address: ")
                        Console.ResetColor()
                        Console.WriteLine("{0}", sdr("address").ToString)
                        Console.WriteLine("")

                        Console.ForegroundColor = ConsoleColor.DarkYellow
                        Console.Write("                   Age: ")
                        Console.ResetColor()
                        Console.WriteLine("{0}", sdr("U_age").ToString)
                        Console.WriteLine("")

                        Console.ForegroundColor = ConsoleColor.DarkYellow
                        Console.Write("                   Marital Status: ")
                        Console.ResetColor()
                        Console.WriteLine("{0}", sdr("status").ToString)
                        Console.WriteLine("")

                        Console.ForegroundColor = ConsoleColor.DarkYellow
                        Console.Write("                   Phone Number: ")
                        Console.ResetColor()
                        Console.WriteLine("+639{0}", sdr("pnumber").ToString)
                        Console.WriteLine("")
                        Console.ForegroundColor = ConsoleColor.DarkYellow

                        Console.Write("                   Email Address: ")
                        Console.ResetColor()
                        Console.WriteLine("{0}", sdr("email").ToString)
                        Console.WriteLine("")


                        sdr.Close()
                        'ask what to do next
                        Dim yorn As String
                        Console.Write("                  Do you want to search another user?  Y/N: ")
                        yorn = Console.ReadKey().Key

                        Do While yorn <> ConsoleKey.Y And yorn <> ConsoleKey.N

                            Console.Write("                  Do you want to search another user?  Y/N: ")
                            yorn = Console.ReadKey().Key
                            Console.WriteLine()
                        Loop

                        If yorn = ConsoleKey.Y Then
                            Console.WriteLine("")
                            SearchFunc()

                        ElseIf yorn = ConsoleKey.N Then
                            MyConnection.Close()
                            Console.Clear()
                            Console.WriteLine("     Operation Canceled :)")
                            Dashboard()
                        End If

                    End While
                    MyConnection.Close()


                Else
                    Console.WriteLine("")
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("                       Account doesnt exist :(")
                    Console.ResetColor()
                    SearchFunc()
                End If

                MyConnection.Close()

            Catch ex As Exception
                Console.WriteLine("error in search is : " & ex.Message)
            End Try
        End If


        Return True
    End Function

    'ADMIN DASHBOARD (OVERRIDES | POLYMORPHISM)
    Public Overrides Function Dashboard()

        Dim selectme As String

        'Console.Clear()
        Dim slct, emp1, emp2, emp3, emp4, emp5 As String
            Dim choice, choice1 As String
            Dim line1, line2, topline As String

        'Console Title Bar
        Console.Title = "      ADMIN DASHBOARD"

        line1 = "                      ="
            line2 = "                         ="
            topline = "||========================================================================================================||"
            emp1 = "  aaaa       ddddddd       mm       mm     ii     nn    nn"
            emp2 = "aa    aa     dd     dd     mm mm mm mm     ii     nnnn  nn"
            emp3 = "aaaaaaaa     dd      dd    mm   m   mm     ii     nn  nnnn"
            emp4 = "aa    aa     dd     dd     mm       mm     ii     nn   nnn"
            emp5 = "aa    aa     ddddddd       mm       mm     ii     nn    nn"

            slct = "<{ SELECT OPTION }>"
            Console.Beep()
            Console.WriteLine("")
            Console.WriteLine()
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
        Console.WriteLine("                      Current Admin: {0}", curradmin)

        Console.WriteLine("")
            Console.ForegroundColor = ConsoleColor.DarkGray
            Console.WriteLine("                                                                                                                        [Q] LOGOUT ")
            Console.ResetColor()
            Console.ForegroundColor = ConsoleColor.Cyan
            Console.WriteLine("")
            Console.WriteLine("")
            Console.WriteLine(slct.PadLeft((Console.WindowWidth / 2) + (slct.Length / 2)))
            Console.WriteLine("")
            Console.WriteLine("")
            Console.ResetColor()


            choice = " [A] CREATE EMPLOYEE ACCOUNT      [B] EDIT ACCOUNT     "
            choice1 = " [C] SEARCH EMPLOYEE              [D] DELETE EMPLOYEE  "


            Console.ForegroundColor = ConsoleColor.DarkYellow
            Console.WriteLine(choice.PadLeft((Console.WindowWidth / 2) + (choice.Length / 2)))
            Console.WriteLine("")
            Console.WriteLine(choice1.PadLeft((Console.WindowWidth / 2) + (choice1.Length / 2)))
            Console.ResetColor()
            Console.WriteLine("")
            Console.WriteLine("")
            Console.WriteLine("")

            Console.ForegroundColor = ConsoleColor.Cyan
            Console.SetCursorPosition(10, 25)
            Console.Write("  SELECT CHOICE: ")
            Console.ResetColor()
            Console.ForegroundColor = ConsoleColor.White
            selectme = Console.ReadLine()
            Console.WriteLine()
            Console.ResetColor()



            Select Case selectme.ToString.ToUpper
                Case "A"
                    Console.WriteLine("")
                    Console.ForegroundColor = ConsoleColor.DarkCyan
                    Console.WriteLine("               CREATE EMPLOYEE:")
                    Console.ResetColor()
                    Console.WriteLine("")
                    Console.ForegroundColor = ConsoleColor.DarkGray
                    Console.WriteLine("                                                                                                                        [9] RETURN")
                    Console.ResetColor()
                    Console.WriteLine("")
                    Create()
                Case "B"
                    Dim opt As String

                    Console.WriteLine("")
                    Console.WriteLine("               SELECT OPTION:")
                    opt = "[1] EDIT EMPLOYEE      [2] EDIT ADMIN"

                    Console.ForegroundColor = ConsoleColor.DarkGray
                    Console.WriteLine("                                                                                                                        [9] RETURN")
                    Console.ResetColor()

                    Console.ForegroundColor = ConsoleColor.DarkYellow
                    Console.WriteLine(opt.PadLeft((Console.WindowWidth / 2) + (opt.Length / 2)))
                    Console.ResetColor()
                    Console.WriteLine("")
                    Console.WriteLine("")
                    EditInterface()

                Case "C"
                    Console.WriteLine()
                    Console.ForegroundColor = ConsoleColor.DarkCyan
                    Console.WriteLine("               SEARCH EMPLOYEE")
                    Console.ResetColor()
                    SearchFunc()
                Case "D"
                    Console.WriteLine("")
                    Console.ForegroundColor = ConsoleColor.DarkCyan
                    Console.WriteLine("               DELETE ACCOUNT")
                    Console.WriteLine("")
                    Console.ResetColor()
                    DeleteAccount()
                Case "Q"
                    Console.Clear()
                    Dim userclass As New User
                    userclass.Logindesign()

                Case Else
                Console.Clear()
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("     Input must be on the list")
                Console.ResetColor()
                Dashboard()

            End Select


            Return True
    End Function

End Class
