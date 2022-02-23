Imports System.Data.OleDb
Public Class Employee : Inherits User
    Public currEmployee As String
    Public currEmployeeID As Integer

    'Property to get current employee
    Public Property CurrentEmployee As String
        Get
            Return currEmployee
        End Get
        Set(value As String)
            currEmployee = value
        End Set
    End Property
    'Property to get current employee ID
    Public Property CurrentEmployeeID As Integer
        Get
            Return currEmployeeID
        End Get
        Set(value As Integer)
            currEmployeeID = value
        End Set
    End Property


    'Update first middle and last name function
    Private Function Fml(ByVal fname As String, ByVal mname As String, ByVal lname As String)
        Try
            MySQL = "UPDATE [emp] SET [Fname]=?, [Mname]=?, [Lname]=? where UserID=?"
            MyConnection = New OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=empdb.mdb")
            MyCommand = New OleDbCommand(MySQL, MyConnection)
            MyConnection.Open()
            MyCommand.Parameters.AddWithValue("?", fname)
            MyCommand.Parameters.AddWithValue("?", mname)
            MyCommand.Parameters.AddWithValue("?", lname)
            MyCommand.Parameters.AddWithValue("?", currEmployeeID)
            MyCommand.ExecuteNonQuery()
            MyConnection.Close()
            Console.Clear()
            Console.WriteLine("Full Name Updated :)")
            UpdateInfo()

        Catch ex As Exception
            Console.WriteLine("error in update " & ex.Message)
        End Try
        Return True
    End Function

    'Update address
    Private Function UpdateAddress(ByVal add As String)
        Try
            MySQL = "UPDATE [emp] SET [address]=? where UserID=?"
            MyConnection = New OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=empdb.mdb")
            MyCommand = New OleDbCommand(MySQL, MyConnection)
            MyConnection.Open()
            MyCommand.Parameters.AddWithValue("?", add)
            MyCommand.Parameters.AddWithValue("?", currEmployeeID)
            MyCommand.ExecuteNonQuery()
            MyConnection.Close()
            Console.Clear()
            Console.WriteLine("Address Updated :)")
            UpdateInfo()

        Catch ex As Exception
            Console.WriteLine("error in adress update " & ex.Message)
        End Try
        Return True
    End Function

    'Update age
    Private Function UpdateAge(ByVal age As String)
        Try
            MySQL = "UPDATE [emp] SET [U_age]=? where UserID=?"
            MyConnection = New OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=empdb.mdb")
            MyCommand = New OleDbCommand(MySQL, MyConnection)
            MyConnection.Open()
            MyCommand.Parameters.AddWithValue("?", CInt(age))
            MyCommand.Parameters.AddWithValue("?", currEmployeeID)
            MyCommand.ExecuteNonQuery()
            MyConnection.Close()
            Console.Clear()
            Console.WriteLine("Age Updated :)")
            UpdateInfo()

        Catch ex As Exception
            Console.WriteLine("error in age update " & ex.Message)
        End Try
        Return True
    End Function

    Public Function Check(ByRef name As String, ByVal sdr As String)
        If Trim(name) = "" Then
            name = sdr
            Return name
        End If
        Return Nothing
    End Function

    'fetch information
    Private Function Fetch()
        Try


            MyConnection.Open()
            MySQL = "SELECT * FROM [emp] WHERE [UserID] =@N"
            MyCommand = New OleDbCommand(MySQL, MyConnection)
            MyCommand.CommandType = CommandType.Text
            MyCommand.Parameters.AddWithValue("@N", currEmployeeID)
            sdr = MyCommand.ExecuteReader

            If sdr.HasRows Then


                While sdr.Read
                    Console.Clear()
                    Console.SetCursorPosition(65, 3)
                    Console.ForegroundColor = ConsoleColor.Cyan
                    Console.WriteLine("UPDATE INFORMATION")
                    Console.ResetColor()
                    Console.WriteLine("")
                    Console.WriteLine("")

                    Console.ForegroundColor = ConsoleColor.DarkYellow
                    Console.Write("               [A] Full Name: ")
                    Console.ResetColor()
                    Console.WriteLine("{0}  {1}  {2}", sdr("Fname").ToString, sdr("Mname").ToString, sdr("Lname").ToString)
                    Console.WriteLine("")

                    Console.ForegroundColor = ConsoleColor.DarkYellow
                    Console.Write("               [B] Address: ")
                    Console.ResetColor()
                    Console.WriteLine("{0}", sdr("address").ToString)
                    Console.WriteLine("")

                    Console.ForegroundColor = ConsoleColor.DarkYellow
                    Console.Write("               [C] Age: ")
                    Console.ResetColor()
                    Console.Write("{0}", sdr("U_age").ToString)

                    Console.ForegroundColor = ConsoleColor.DarkYellow
                    Console.SetCursorPosition(70, 10)
                    Console.Write("[D] Marital Status: ")
                    Console.ResetColor()
                    Console.WriteLine("{0}", sdr("status").ToString)
                    Console.WriteLine("")

                    Console.ForegroundColor = ConsoleColor.DarkYellow
                    Console.Write("               [E] Phone Number: ")
                    Console.ResetColor()
                    Console.Write("+639{0}", sdr("pnumber").ToString)

                    Console.ForegroundColor = ConsoleColor.DarkYellow
                    Console.SetCursorPosition(70, 12)
                    Console.Write("[F] Email Address: ")
                    Console.ResetColor()
                    Console.WriteLine("{0}", sdr("email").ToString)
                    Console.WriteLine("")



                End While
                MyConnection.Close()

            Else
                Console.WriteLine("")
                Console.WriteLine("Account doesnt exist :(")
            End If


        Catch ex As Exception
            Console.WriteLine("Error while fetching" & ex.Message)
        End Try
        Return Nothing
    End Function

    'Updata info interface
    Private Function UpdateInfo()

        Try
            Dim updateinput As String
            Dim f_name, m_name, l_name, add, mail, ageup, stat As String

            Fetch()


            Console.WriteLine()
            Console.WriteLine()
            Console.WriteLine()

            Do While updateinput <> "Y" And updateinput <> "N"
                Console.Write("       Update Information? Y/N : ")
                updateinput = Console.ReadLine().ToString.ToUpper
                Returndashboard(updateinput)
            Loop



            MyConnection.Open()
            MySQL = "SELECT * FROM [emp] WHERE [UserID] =@N"
            MyCommand = New OleDbCommand(MySQL, MyConnection)
            MyCommand.CommandType = CommandType.Text
            MyCommand.Parameters.AddWithValue("@N", currEmployeeID)
            sdr = MyCommand.ExecuteReader


            If sdr.Read Then
                f_name = sdr("Fname").ToString
                m_name = sdr("Mname").ToString
                l_name = sdr("Lname").ToString
                mail = sdr("email").ToString
                stat = sdr("status").ToString
                add = sdr("address").ToString
                ageup = sdr("U_age").ToString

                Select Case updateinput.ToString.ToUpper
                    Case "Y"
                        Dim selection As String
                        Console.WriteLine()
                        While selection <> "A" And selection <> "B" And selection <> "C"
                            Console.Write("    Edit Info?: ")
                            selection = Console.ReadLine().ToUpper
                        End While


                        If Trim(selection).ToUpper = "A" Then
                            Dim newfname, newmname, newlname As String

                            Console.Write("          Enter Firstname: ")
                            newfname = Console.ReadLine()
                            Check(newfname, f_name)

                            Console.Write("          Enter Middlename: ")
                            newmname = Console.ReadLine()
                            Check(newmname, m_name)

                            Console.Write("          Enter Lastname: ")
                            newlname = Console.ReadLine()
                            Check(newlname, l_name)

                            Fml(newfname, newmname, newlname)

                        ElseIf Trim(selection).ToUpper = "B" Then

                            Dim address As String

                            Console.Write("          Enter Address: ")
                            address = Console.ReadLine()
                            Check(address, add)

                            Return UpdateAddress(address)

                        ElseIf Trim(selection).ToUpper = "C" Then
                            Dim age As String

                            Console.Write("          Enter Age: ")
                            age = Console.ReadLine()
                            Check(age, ageup)
                            Return UpdateAge(age)


                        Else
                            Console.Write("     Edit Info?: ")
                            selection = Console.ReadLine()
                        End If



                    Case "N"
                        MyConnection.Close()
                        Console.Clear()
                        Dashboard()

                    Case "Q"
                        MyConnection.Close()
                        Console.Clear()
                        Return Dashboard()
                End Select
            End If


        Catch ex As Exception
            Console.WriteLine("updateinfo error " & ex.Message)
        End Try
        Return Nothing
    End Function

    'return to dashboard
    Private Function Returndashboard(ByVal input As String)
        If input = "Q" Then
            Console.Clear()
            Dashboard()
        End If
        Return True
    End Function

    'Change password
    Private Function Changepassword()

        Console.SetCursorPosition(120, 25)
        Console.ForegroundColor = ConsoleColor.DarkGray
        Console.WriteLine("[Q] CANCEL")
        Console.ResetColor()

        Dim selection As Char


        While selection <> "Y" And selection <> "N"
            Console.WriteLine("")
            Console.ForegroundColor = ConsoleColor.Yellow
            Console.Write("     Do you really want to change Password? Y/N : ")
            Console.ResetColor()
            selection = Console.ReadLine.ToUpper
            Returndashboard(selection)

        End While

        If selection = "Y" Then
            Try
                Dim newuserpass As String

                Console.WriteLine("")

                Console.Write("        Enter New Password (Case Sensitive): ")
                newuserpass = Console.ReadLine
                Returndashboard(newuserpass)
                Console.WriteLine("")

                While newuserpass.Length = 0

                    Console.WriteLine("")

                    Console.Write("        Enter New Password (Case Sensitive): ")
                    newuserpass = Console.ReadLine.ToUpper
                    Returndashboard(newuserpass)

                    Console.WriteLine("")
                End While

                MySQL = "UPDATE [emp] SET [Upassword]=? where UserID=?"
                MyCommand = New OleDbCommand(MySQL, MyConnection)
                MyConnection.Open()
                MyCommand.Parameters.AddWithValue("?", newuserpass)
                MyCommand.Parameters.AddWithValue("?", currEmployeeID)
                MyCommand.ExecuteNonQuery()

                MyConnection.Close()
                Console.Clear()
                Console.ForegroundColor = ConsoleColor.DarkGreen
                Console.WriteLine("Successfully Changed :)")
                Console.ResetColor()

                Return Dashboard()


            Catch ex As Exception
                Console.WriteLine("failed" & ex.Message)
            End Try
        ElseIf selection = "N" Then
            Console.Clear()
            Console.WriteLine("Operation Canceled :)")
            Dashboard()

        End If


        Return Nothing
    End Function

    'Apply Leave 
    Private Function Applyleave()
        Console.WriteLine("")
        Console.WriteLine("                                                                     APPLY FOR LEAVE")
        Console.WriteLine("")


        Dim reason, days, workplan As String

        Do While reason = "" Or days = "" Or workplan = ""
            Console.WriteLine("")
            Console.WriteLine("                         Fill in the following: ")
            Console.WriteLine()
            Console.WriteLine()
            Console.Write("          Reason for leave: ")
            reason = Console.ReadLine()

            Console.Write("          How may days: ")
            days = Console.ReadLine()

            Console.Write("          What is your current work plan: ")
            workplan = Console.ReadLine()

        Loop

        Try
            Dim leavedate As Date = Format(Now, "dddd,d MMM yyyy")
            MySQL = "INSERT INTO [leave] ([Leave_ID],[Reason],[Days],[Workplan],[Leave_date]) " &
            "VALUES (?,?,?,?,?)"
            MyConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=empDB.mdb")
            MyCommand = New OleDbCommand(MySQL, MyConnection)
            MyConnection.Open()
            MyCommand.Parameters.AddWithValue("?", CInt(currEmployeeID))
            MyCommand.Parameters.AddWithValue("?", reason)
            MyCommand.Parameters.AddWithValue("?", days)
            MyCommand.Parameters.AddWithValue("?", workplan)
            MyCommand.Parameters.AddWithValue("?", leavedate)
            MyCommand.ExecuteNonQuery()

            MyConnection.Close()

            Console.Clear()
            Console.WriteLine("Succesful application")
            Return Dashboard()

        Catch ex As Exception

        End Try
        Return Nothing
    End Function

    'salary total
    Private Function Takehome(ByRef a As Double,
                              ByRef b As Double,
                              ByRef c As Double,
                              ByRef d As Double,
                              ByRef salary As Double)

        Return (salary - (a + b + c + d)).ToString("Php ###,###,###.00")
    End Function

    'Payslip function
    Private Function Payslip()
        Console.Clear()
        Console.SetCursorPosition(70, 2)
        Console.ForegroundColor = ConsoleColor.DarkCyan
        Console.WriteLine("P A Y S L I P")
        Console.ResetColor()
        Console.WriteLine()
        Console.WriteLine()
        Try
            MyConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=empDB.mdb")
            MyConnection.Open()
            MySQL = "SELECT * FROM [emp] WHERE [UserID] =@N"
            MyCommand = New OleDbCommand(MySQL, MyConnection)
            MyCommand.CommandType = CommandType.Text
            MyCommand.Parameters.AddWithValue("@N", currEmployeeID)
            sdr = MyCommand.ExecuteReader

            If sdr.Read Then
                Dim namefromdb As String



                namefromdb = sdr("Fname").ToString & " " & sdr("Mname").ToString & " " & sdr("Lname").ToString
                Console.SetCursorPosition(127, 3)
                Console.WriteLine(Format(Now, "dddd,d MMM yyyy"))
                Console.SetCursorPosition(105, 8)
                Console.WriteLine("1 Month Salary Period")
                Console.WriteLine("")
                Console.WriteLine("")
                Console.Write("                    Name: ")
                Console.ForegroundColor = ConsoleColor.DarkYellow
                Console.WriteLine(namefromdb)
                Console.ResetColor()
                Console.WriteLine("")
                Console.WriteLine("")
                Console.WriteLine("")
                Console.WriteLine("                    Basic Salary {0,93}", sdr("salary"))
                Console.WriteLine("")
                Console.WriteLine("                    DEDUCTION --------------------------------------------------------------------------------------------  {0}", sdr("deduction"))
                Console.WriteLine("")
                Console.WriteLine("                    SSS          {0,93}", sdr("sss").ToString)
                Console.WriteLine("")
                Console.WriteLine("                    PhilHealth   {0,93}", sdr("phealth").ToString)
                Console.WriteLine("")
                Console.WriteLine("                    Pag-Ibig     {0,93}", sdr("pibig").ToString)
                Console.WriteLine("")
                Console.WriteLine("                    Tax          {0,93}", sdr("tax").ToString)
                Console.WriteLine("")
                Console.WriteLine("                    __________________________________________________________________________________________________________")
                Console.WriteLine("")
                Console.WriteLine("                    Total Salary {0,93}", Takehome(sdr("tax"), sdr("sss"), sdr("phealth"), sdr("pibig"), sdr("salary")))

            End If

            MyConnection.Close()

            Console.WriteLine("")
            Console.WriteLine("")
            Console.WriteLine("")
            Console.ForegroundColor = ConsoleColor.DarkGray
            Console.Write("    Press Enter to back       ")
            Console.ReadLine()
            Console.Clear()
            Return Dashboard()
        Catch ex As Exception
            Console.WriteLine("Error in name fetch: " & ex.Message)
        End Try
        Console.ReadLine()
        Return Nothing
    End Function

    Public Overrides Function Dashboard() As Object
        Console.Beep()
        Dim inputval, slct, emp1, emp2, emp3, emp4, emp5 As String
        Dim choice, choice1 As String
        Dim line1, line2, topline As String

        'Console Title Bar
        Console.Title = "   EMPLOYEE " & currEmployee & " with ID - " & currEmployeeID

        line1 = "                      =       "
        line2 = "     ="
        topline = "||========================================================================================================||"
        emp1 = "eeeeee     mm       mm     pppppp      ll           oooo       yyy    yyy     eeeeee    eeeeee"
        emp2 = "ee         mm mm mm mm     pp    p     ll         oo    oo       yy  yy       ee        ee    "
        emp3 = "eeee       mm   m   mm     pppppp      ll         oo    oo         yy         eeee      eeee  "
        emp4 = "ee         mm       mm     pp          ll         oo    oo         yy         ee        ee    "
        emp5 = "eeeeee     mm       mm     pp          llllll       oooo           yy         eeeeee    eeeeee"


        slct = "<{ SELECT OPTION }>"

        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("")

        Console.ForegroundColor = ConsoleColor.Gray
        Console.WriteLine(topline.PadLeft((Console.WindowWidth / 2) + (topline.Length / 2)))
        Console.WriteLine(line1 & "                                                                                              " & line2)
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
        Console.WriteLine(line1 & "                                                                                              " & line2)
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
        Console.WriteLine("")
        Console.ResetColor()


        choice = "[1] UPDATE INFORMATION             [3] APPLY LEAVE"
        choice1 = "[2] CHANGE PASSWORD                [4] VIEW PAYSLIP"


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
        inputval = CStr(Console.ReadLine())
        Console.ResetColor()



        While inputval.Length = 0
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.DarkRed
            Console.WriteLine(" Please enter a value!")
            Console.ResetColor()
            Dashboard()

        End While

        Do While inputval <> "1" And inputval <> "2" And inputval <> "3" And inputval <> "4" And inputval <> "0"
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.DarkRed
            Console.WriteLine(" Input must be on the list or not empty")
            Console.ResetColor()
            Dashboard()
        Loop


        Select Case inputval
            Case "1"
                UpdateInfo()
            Case "2"

                Changepassword()

            Case "3"
                Applyleave()

            Case "4"
                Payslip()

            Case "0"
                Console.Clear()
                Dim userclass As New User
                userclass.Logindesign()
        End Select
        Return True

    End Function
End Class
