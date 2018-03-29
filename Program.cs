/*
Name: CalculatingCostOrder
Course: PROG1783-18W-Sec1-IT Support Programming Fundamentals
Professor: Scanlan, H.
Number Student ID: 7679566
Student: Daniel Brazil
Date: Feb, 20, 2018
*/


using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;
using System.Linq;


namespace ClotheSale
{
    //This class is responsible for executing the system
    class Program
    {

        //method main
        static void Main(string[] args)
        {
            Screen s = new Screen();
            s.SystemProcess();
        }

    }

    class Screen
    {
        #region Declaration of Constants

        //this constant use to define where start left cursor
        const int LEFTPOSITIONSCREEN = 2;
        //this constant define the price of tax
        const double SALETAX = 13.00;

        #endregion

        #region Declaration of Variables

        //variable to use for receive answer about some questions
        private string sInputAnswer;

        public string InputAnswer
        {
            get
            {
                return sInputAnswer;
            }
            set
            {
                sInputAnswer = value;
            }
        }
        //variable to receive answer about crazy lottery - this variable is used for Assignment 3
        private string sAnswerCrazyLottery;

        public string AnswerCrazyLottery
        {
            get
            {
                return sAnswerCrazyLottery;
            }
            set
            {
                sAnswerCrazyLottery = value;
            }
        }

        //variable to use for calculate total purchase
        private double dTotalOrder;

        public double TotalOrder
        {
            get
            {
                return dTotalOrder;
            }
            set
            {
                dTotalOrder = value;
            }
        }

        //variable to use for calculate total purchase but plus tax
        private double dTotalOrderPlusTax;

        public double TotalOrderPlusTax
        {
            get
            {
                return dTotalOrderPlusTax;
            }
            set
            {
                dTotalOrderPlusTax = value;
            }
        }


        //variable to receive the value of discount random about crazy lottery
        private double dDiscountRandom;

        public double DiscountRandom
        {
            get
            {
                return dDiscountRandom;
            }
            set
            {
                dDiscountRandom = value;
            }
        }


        #endregion

        #region Methods and Functios

        public void SystemProcess()
        {
            //variable to use for control if system can continue another steps
            bool bContinue = false;
            //variable to control more than 1 purchase
            string sYesNo = "Y";
            //this variable receive the last line write in console
            int iLastLine = 0;

            //set culture because I am brazilian and in my home my VS is in brazilian and type of date and currency is different 
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            //set the title of program
            Console.Title = "Clothes Sale";

            //loop for question more than 1 purchase
            while (sYesNo.ToUpper() == "Y")
            {
                //clear the screen
                Console.Clear();
                //call the method to create borders in the system
                CreateBorders(iLastLine);
                //call the method to create welcome screen 
                WelcomeScreen();

                //call the method tha ask about new user or register user
                QuestionUserScreen();

                //take information about kind of user - new ou registered user
                Users.eTypeUser typeUser = int.Parse(this.InputAnswer) == Users.eTypeUser.currentuser.GetHashCode() ?
                  Users.eTypeUser.currentuser : Users.eTypeUser.newtuser;

                //create a object of user
                Users.User[] User = new Users.User[1];

                //select what is screen need to show for user
                switch (typeUser)
                {
                    case Users.eTypeUser.currentuser:
                        {
                            //call the method that be login of old user
                            bContinue = LoginUserScreen(ref User, 0);
                            break;
                        }
                    case Users.eTypeUser.newtuser:
                        {
                            //call the method that create a new user
                            bContinue = NewUserScreen(ref User, 0);
                            break;
                        }
                    default:
                        break;
                }

                //if system can create a new user or do sucess login
                if (bContinue)
                {
                    //call the method about question if user like receive a discount - crazy lotery
                    QuestionCrazyLottery();
                }

                if (bContinue)
                {
                    //call the method about what kind of item user like buy and get the last line write in the screen
                    iLastLine = ProcessQuestions(ref User);
                    //change foregroud color
                    Console.ForegroundColor = ConsoleColor.Red;
                    //make condition in according of last line
                    if (iLastLine == 0)
                    {
                        //call method to re draw line 
                        ReDrawLine(35);
                        ReDrawLine(36);
                        //set cursor in position 
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN, 35);
                        Console.Write("Would you like to order another order?");
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN, 36);
                        Console.Write("Press Y (es) for new order OR press ANY key to finish!");
                    }
                    else
                    {
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN, iLastLine + 1);
                        Console.Write("Would you like to order another order?");
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN, iLastLine + 2);
                        Console.Write("Press Y (es) for new order OR press ANY key to finish!");
                    }
                    sYesNo = Console.ReadLine().ToUpper();
                }
                else
                {
                    sYesNo = "N";
                    MesssageAlertScreen("Press ENTER to finalize!!!");
                    Console.ReadLine();
                }
            }
        }

        //method responsible to write welcome screen
        static void WelcomeScreen()
        {
            Console.SetCursorPosition(20, 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Welcome to the Arnold's Cool Clothing Emporium");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 2);
            Console.Write("----------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 3);
            Console.Write("This system is responsible for the sale of clothes.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 4);
            Console.Write("You need to buy at least 2 different items and 1 of them should be shirt.");
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("----------------------------------------------------------------------------");
            Console.SetCursorPosition(0, 0);
        }

        //method responsible to write title system screen
        static void TitleSystemScreen()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(30, 1);
            Console.Write("Clothing Sales System");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(64, 1);
            Console.Write("Version: 1.0");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 2);
            Console.Write("----------------------------------------------------------------------------");
            Console.SetCursorPosition(0, 0);
        }

        //method responsible ask about crazy lottery - assignmnet 3
        public void QuestionCrazyLottery()
        {

            AnswerCrazyLottery = "?";

            //until the answer is different Y or N the system ask again 
            while (AnswerCrazyLottery.ToUpper() != "Y" && AnswerCrazyLottery.ToUpper() != "N")
            {
                //call the method that re write 1 line
                ReDrawLine(5);
                ReDrawLine(6);
                ReDrawLine(7);
                ReDrawLine(8);
                ReDrawLine(9);
                ReDrawLine(10);
                ReDrawLine(11);
                ReDrawLine(12);
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 5);
                Console.Write("Would you like to try a crazy discount on your purchase? [Y]es or [N]o - [ ]");
                Console.SetCursorPosition(76, 5);
                AnswerCrazyLottery = Console.ReadLine().ToUpper();
                if (AnswerCrazyLottery.ToUpper() != "Y" && AnswerCrazyLottery.ToUpper() != "N")
                    MesssageAlertScreen("Would you like to try a crazy discount on your purchase? [Y]es or [N]o - [ ]");
                else
                    MesssageAlertScreen("");
                Console.SetCursorPosition(76, 5);
            }
            Console.SetCursorPosition(0, 0);
        }

        //method responsible about question of new user or old user
        public void QuestionUserScreen()
        {

            InputAnswer = "?";
            //until the answer is different 1 or 2 the system ask again
            while (InputAnswer != "1" && InputAnswer != "2")
            {
                //call the method that re write 1 line
                ReDrawLine(6);
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 6);
                Console.Write("Press [1] for Register Users OR [2] for New User: [ ]");
                Console.SetCursorPosition(53, 6);
                InputAnswer = Console.ReadLine();
                if (InputAnswer != "1" && InputAnswer != "2")
                    MesssageAlertScreen("Please Press [1] for Register Users OR [2] for New User.");
                else
                    MesssageAlertScreen("");
                Console.SetCursorPosition(53, 6);
            }
            Console.SetCursorPosition(0, 0);
        }

        //method responsible about screen of login old user
        public bool LoginUserScreen(ref Users.User[] user, int iLastLine)
        {
            //variable that control if find user in file user.txt
            bool bFindUser = false;
            //variable that control if password is correct
            bool bValidPwd = false;
            //variable that control how many try user input the login and password
            int iTemp = 1;

            //create a object the user of class users
            Users users = new Users();

            //clear the screen
            Console.Clear();
            //call the method that create borders
            CreateBorders(iLastLine);
            //call the method that create title system
            TitleSystemScreen();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(40, 3);
            Console.Write("Login");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 4);
            Console.Write("----------------------------------------------------------------------------");
            //if do not find user ask again and show message that did not find
            for (iTemp = 0; iTemp <= 2; iTemp++)
            {
                ReDrawLine(5);
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 5);
                Console.Write("User name: ");
                Console.SetCursorPosition(13, 5);
                InputAnswer = Console.ReadLine();
                //call the method that find if user exist in file user.txt
                user = users.FindUser(InputAnswer);
                //set variable in according that information created for the class user
                bFindUser = user[0].username != string.Empty && user[0].username != null;
                if (bFindUser)
                {
                    //clear message alert
                    MesssageAlertScreen("");
                    //set variable to exit for loop
                    iTemp = 2;
                }
                else
                {
                    //validate how many tries user input username
                    if (iTemp == 2)
                    {
                        //show alert in screen 
                        MesssageAlertScreen("Login attempt exceeded. System will finish!!!");
                    }
                    else
                        //show alert in screen 
                        MesssageAlertScreen("User do not find, please input a valid user name!");
                }
            }

            //if user is find continue and ask about password
            if (bFindUser)
            {
                for (iTemp = 0; iTemp <= 2; iTemp++)
                {
                    //call the method that re write the line
                    ReDrawLine(6);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(LEFTPOSITIONSCREEN, 6);
                    Console.Write("Password : ");
                    Console.SetCursorPosition(13, 6);
                    InputAnswer = Console.ReadLine();
                    //call the method that validate user and password in file user.txt
                    bValidPwd = users.ValidUserAndPassword(ref user, InputAnswer);
                    if (bValidPwd)
                    {
                        //clear message alert
                        MesssageAlertScreen("");
                        iTemp = 2;
                    }
                    else
                    {
                        if (iTemp == 2)
                        {
                            //show alert in screen 
                            MesssageAlertScreen("Login attempt exceeded. System will finish!!!");
                        }
                        else
                            //show alert in screen 
                            MesssageAlertScreen("Password invalid. Try Again!!!");
                    }
                }
            }
            //if user and password exits continue
            if (bFindUser && bValidPwd)
                return true;
            else
                return false;
        }

        //method responsible for new user
        public bool NewUserScreen(ref Users.User[] newUser, int iLastLine)
        {
            //variable responsible for control if system can continue
            bool bContinue = false;
            //variable responsible for control if user there is in file user.txt
            bool bFindUser = false;

            //create object of a class Utils
            Utils util = new Utils();

            //crate a object of stringbuilder. This stringbuilder make sequence of information about user in line to save a new user
            StringBuilder sb = new StringBuilder();
            //variable to control how many line exist in file of user.txt
            int iCounter = 0;
            //variable to take line per line of file user.txt
            string sLine;

            //create a new object of class Users
            Users users = new Users();

            //set global variable with blank
            InputAnswer = string.Empty;
            //clear screen
            Console.Clear();
            //call method to create borders
            CreateBorders(iLastLine);
            //call method to create title system 
            TitleSystemScreen();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(30, 3);
            Console.Write("New User Registration");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 4);
            Console.Write("----------------------------------------------------------------------------");
            //call method that ask about name of new user
            InputAnswer = QuestionUserName();
            while (!bContinue)
            {
                //validate if username more or equals than 15 caracteres
                if (InputAnswer.Length > 16)
                {
                    //show alert in screen 
                    MesssageAlertScreen("User name should be less than 15 characters!!!");
                    //call the method that re write the line
                    ReDrawLine(5);
                    //call method that ask about name of new user
                    InputAnswer = QuestionUserName();
                }
                //validate if user there is in file user.txt
                else if (users.FindUser(InputAnswer)[0].username == InputAnswer)
                {
                    //show alert in screen 
                    MesssageAlertScreen("User already exists!!!");
                    //call the method that re write the line
                    ReDrawLine(5);
                    //call method that ask about name of new user
                    InputAnswer = QuestionUserName();
                }
                //validate if there is blank spaces in username
                else if (InputAnswer.IndexOf(" ") > 0)
                {
                    //show alert in screen 
                    MesssageAlertScreen("Username can not have space!!!");
                    //call the method that re write the line
                    ReDrawLine(5);
                    //call method that ask about name of new user
                    InputAnswer = QuestionUserName();
                }
                //if pass for every conditions above, system can continue
                else
                {
                    //clear message alert
                    MesssageAlertScreen("");
                    //set variable to continue
                    bContinue = true;
                    //input username in object of user in property username
                    newUser[0].username = InputAnswer;
                }
            }

            //if syste can continue
            if (bContinue)
            {
                //set variable with false because this variable will be used in this block
                bContinue = false;
                //call method that ask about full name
                InputAnswer = QuestionFullName();

                while (!bContinue)
                {
                    //if user full name more or equals than 40 caracteres
                    if (InputAnswer.Length > 41)
                    {
                        //show alert in screen 
                        MesssageAlertScreen("Full name should be less than 40 characters!!!");
                        //call the method that re write the line
                        ReDrawLine(6);
                        //call method that ask about full name
                        InputAnswer = QuestionFullName();
                    }
                    else
                    {
                        //clear message alert
                        MesssageAlertScreen("");
                        //set variable to continue
                        bContinue = true;
                        //input username in object of user in property fullname
                        newUser[0].fullname = InputAnswer;
                    }
                }
            }

            //if syste can continue
            if (bContinue)
            {
                //set variable with false because this variable will be used in this block
                bContinue = false;

                //call method that ask about birthday
                InputAnswer = QuestionBday();
                while (!bContinue)
                {
                    //DateTime dateValue;
                    //this validation verify if user input a correct date

                    // I commented this validation because in my laptop I used portuguese version and in college I used english version
                    //and sometimes validation don't working correct. I set localization, but don't working
                    //if (DateTime.TryParse(InputAnswer, out dateValue))
                    //{
                    //    //show alert in screen 
                    //    MesssageAlertScreen("Invalid date format!!! Format should be mm/dd/yyyy .");
                    //    //call the method that re write the line
                    //    ReDrawLine(7);
                    //    //call method that ask about birthday
                    //    InputAnswer = QuestionBday();
                    //}
                    //else
                    //{
                        //clear message alert
                        MesssageAlertScreen("");
                        //set variable to continue
                        bContinue = true;
                        //input username in object of user in property bday
                        newUser[0].bday = InputAnswer; ;
                    //}
                }
            }

            //if syste can continue
            if (bContinue)
            {
                //set variable with false because this variable will be used in this block
                bContinue = false;

                //call method that ask about password
                InputAnswer = QuestionPassword();
                while (!bContinue)
                {
                    //if password more or equals than 10 caracteres
                    if (InputAnswer.Length > 11)
                    {
                        //show alert in screen 
                        MesssageAlertScreen("Password should be less than 10 characters!!!");
                        //call the method that re write the line
                        ReDrawLine(8);
                        //call method that ask about password
                        InputAnswer = QuestionPassword();
                    }
                    //if password less or equals than 3 caracteres
                    else if (InputAnswer.Length < 4)
                    {
                        //show alert in screen 
                        MesssageAlertScreen("Password should be more than 3 characters!!!");
                        //call the method that re write the line
                        ReDrawLine(8);
                        //call method that ask about password
                        InputAnswer = QuestionPassword();
                    }
                    else
                    {
                        //clear message alert
                        MesssageAlertScreen("");
                        //set variable to continue
                        bContinue = true;
                        //input username in object of user in property password
                        newUser[0].pwd = InputAnswer;
                    }
                }
            }

            //if syste can continue - in this code, system just validate if password is the same a repassword
            if (bContinue)
            {
                bContinue = false;

                InputAnswer = QuestionRePassword();
                while (!bContinue)
                {
                    if (InputAnswer != newUser[0].pwd)
                    {
                        //show alert in screen
                        MesssageAlertScreen("Password different from previous!!!");
                        //call the method that re write the line
                        ReDrawLine(9);
                        InputAnswer = QuestionRePassword();
                    }
                    else
                    {
                        //clear message alert
                        MesssageAlertScreen("");
                        //set variable to continue
                        bContinue = true;
                    }
                }
            }

            //if syste can continue
            if (bContinue)
            {
                //call method that ask about if user is Senior or Student
                InputAnswer = QuestionAboutSeniorOrStudent();

                while (InputAnswer.ToUpper() != "Y" && InputAnswer.ToUpper() != "N")
                {
                    //show alert in screen
                    MesssageAlertScreen("Please press [Y]es OR [N]o !!!");
                    //call the method that re write the line
                    ReDrawLine(10);
                    InputAnswer = QuestionAboutSeniorOrStudent();
                }
                //set information about user is student or senior
                newUser[0].studentsenior = InputAnswer.ToUpper() == "Y" ? 1 : 0;
                //set variable to continue
                bContinue = true;
            }

            //verify if user.txt exist
            if (!util.CheckFileExist(util.FileUser))
            {
                //call method that create file and input new user inside the file in last line
                util.CreateFile(util.FileUser);
                sb.AppendLine(newUser[0].username + ";" + newUser[0].fullname + ";" + newUser[0].bday.ToString() +
                  ";" + newUser[0].pwd + ";" + newUser[0].studentsenior + ";");
                //call the method that save user inside file
                util.WriteFile(util.FileUser, sb);
            }
            else
            {
                //if file exists needs verify if user exists in file
                // Read the file and display it line by line.  
                System.IO.StreamReader file = new System.IO.StreamReader(util.FileUser);
                bFindUser = false;
                //loop validate if new user already exist in file user.txt
                while ((sLine = file.ReadLine()) != null)
                {
                    if (sLine.Substring(0, sLine.IndexOf(';')) == newUser[0].username)
                    {
                        bFindUser = true;
                        continue;
                    }
                    iCounter++;
                }
                //all the time is necessary to close file
                file.Close();

                //if find user need show the information for user
                if (bFindUser)
                {
                    bContinue = false;
                    //show alert in screen
                    MesssageAlertScreen("User already exist, please login with the user!!!");
                }
                else
                {
                    //call method that create file and input new user inside the file in last line
                    sb.AppendLine(newUser[0].username + ";" + newUser[0].fullname + ";" + newUser[0].bday +
                      ";" + newUser[0].pwd + ";" + newUser[0].studentsenior + ";");
                    //call the method that save user inside file
                    util.WriteFile(util.FileUser, sb);
                    bContinue = true;
                }
            }

            //if syste can continue
            if (bContinue)
            {
                //call the method that ask if user like do purchase now
                InputAnswer = QuestionAboutPurchaseNow();

                while (InputAnswer.ToUpper() != "Y" && InputAnswer.ToUpper() != "N")
                {
                    //show alert in screen
                    MesssageAlertScreen("Please press [Y]es OR [N]o !!!");
                    ReDrawLine(11);
                    InputAnswer = QuestionAboutPurchaseNow();
                }
                bContinue = InputAnswer.ToUpper() == "Y" ? true : false;
            }

            return bContinue;
        }

        //method that write the question about username
        public string QuestionUserName()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 5);
            Console.Write("User name: ");
            Console.SetCursorPosition(13, 5);
            InputAnswer = Console.ReadLine();
            return InputAnswer;
        }

        //method that write the question about fullname
        public string QuestionFullName()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 6);
            Console.Write("Full name: ");
            Console.SetCursorPosition(13, 6);
            InputAnswer = Console.ReadLine();
            return InputAnswer;
        }

        //method that write the question about bday
        public string QuestionBday()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 7);
            Console.Write("Birthday : ");
            Console.SetCursorPosition(13, 7);
            InputAnswer = Console.ReadLine();
            return InputAnswer;
        }

        //method that write the question about password
        public string QuestionPassword()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 8);
            Console.Write("Password : ");
            Console.SetCursorPosition(13, 8);
            InputAnswer = Console.ReadLine();
            return InputAnswer;
        }

        //method that write the question about repassword
        public string QuestionRePassword()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 9);
            Console.Write("Retype Password : ");
            Console.SetCursorPosition(20, 9);
            InputAnswer = Console.ReadLine();
            return InputAnswer;
        }

        //method that write the question about if user is student or senior
        public string QuestionAboutSeniorOrStudent()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 10);
            Console.Write("Are you a student or a senior? [Y]es OR [N]o : [ ]");
            Console.SetCursorPosition(50, 10);
            InputAnswer = Console.ReadLine();
            return InputAnswer;
        }

        //method that write the question about if user would like do purchase now
        public string QuestionAboutPurchaseNow()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 11);
            Console.Write("Do you want to make a purchase now? [Y]es OR [N]o : [ ]");
            Console.SetCursorPosition(55, 11);
            InputAnswer = Console.ReadLine();
            return InputAnswer;

        }

        //method that write title user screen
        public void TitleUserScreen(ref Users.User[] user, int iLastLine)
        {
            Console.Clear();
            CreateBorders(iLastLine);
            TitleSystemScreen();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 3);
            Console.Write("Welcome {0} ", user[0].username);
            if (user[0].studentsenior == 1)
            {
                Console.SetCursorPosition(40, 3);
                Console.Write("This customer is {0} ", "Senior Or Student.");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 4);
            Console.Write("----------------------------------------------------------------------------");
            Console.SetCursorPosition(0, 0);
        }

        //method that write title item screen
        public void TitleItemScreen(ref Items.Itens[] item, int iNumberItem)
        {
            Items it = new Items();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 5);
            Console.Write("You are buying: {0} - Price: ${1}", it.TakeTypeOfItem(item[iNumberItem].item), it.TakePriceOfItem(item[iNumberItem].item).ToString());
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 6);
            Console.Write("----------------------------------------------------------------------------");
            Console.SetCursorPosition(0, 0);
        }

        //method that write questions about itens in the screen
        public int ProcessQuestions(ref Users.User[] User)
        {
            //variable to control how many itens was buying
            int iQuantityItens = 1;
            //variable to control what is the number item
            int iNumberItem = 0;
            //variable to control how many lines in file item.txt
            int iCounter = 0;
            //variable to control what is last line
            int iLastLine = 0;
            //variable to receive line of file item.txt
            string sLine;
            //variable to control if user like to buy another item
            string sAnswerBuyNewItem = "";
            //variable to control if user input correct answer
            bool bValidate = true;

            //create a object of struct Itens of class Items
            Items.Itens[] itens = new Items.Itens[iQuantityItens];
            //create a object of class Items
            Items it = new Items();
            //call the method to write user screen
            TitleUserScreen(ref User, 0);
            //create a object of Utils
            Utils utils = new Utils();

            //loop to verify if user input correct information
            while (bValidate)
            {
                //call the method that re write the line
                ReDrawLine(16);
                //call method that return type of item
                ReturnTypeItem(ref itens, iNumberItem);

                //make validation about purchase
                //user need buy always shirt
                //user cannot buy the same item
                //user need buy at least 2 items
                if (itens.Length >= 2 && FindIfBuyingItem(ref itens, itens[iNumberItem].item))
                {
                    MesssageAlertScreen("Purchased items must be different!!!");
                }
                else if (itens.Length <= 2 && itens[iNumberItem].item == Items.eTypeOfItem.checkout)
                {
                    MesssageAlertScreen("You have to buy at least 2 items!!!");
                }
                else if (itens[iNumberItem].item == Items.eTypeOfItem.checkout && !FindIfBuyingItem(ref itens, Items.eTypeOfItem.shirt))
                {
                    MesssageAlertScreen("You need to buy a shirt.");
                }
                //if user pass in above validation and the option is checkout, the system show results
                else if (itens.Length > 2 && itens[iNumberItem].item == Items.eTypeOfItem.checkout)
                {
                    iLastLine = 0;
                    //call the method that show results
                    iLastLine = ShowResults(ref itens, ref User, true);
                    //CreateBorders(iLastLine);
                    bValidate = false;
                }
                //if user input option of historic of last purchases
                else if (itens[iNumberItem].item == Items.eTypeOfItem.historic)
                {
                    //create a object of items and read file item.txt
                    Items.Itens[] itensHistoric = new Items.Itens[1];
                    //validate if file exist
                    if (!utils.CheckFileExist(utils.FileItem))
                    {
                        //show alert in screen
                        MesssageAlertScreen("User do not have Historic of Purchase!!!");
                    }
                    else
                    {
                        //if file exist read file and input information inside object of itensHistoric
                        System.IO.StreamReader file = new System.IO.StreamReader(utils.FileItem);
                        iCounter = 0;
                        //read line per line
                        while ((sLine = file.ReadLine()) != null)
                        {
                            if (sLine != "")
                            {
                                //take each part of line that contain a information about item purchase
                                itensHistoric[iCounter].userPurchase = sLine.Substring(0, sLine.IndexOf(';'));
                                sLine = sLine.Remove(0, sLine.IndexOf(';') + 1);
                                itensHistoric[iCounter].guidOrder = sLine.Substring(0, sLine.IndexOf(';'));
                                sLine = sLine.Remove(0, sLine.IndexOf(';') + 1);
                                itensHistoric[iCounter].item = it.TakeTypeOfItemByName(sLine.Substring(0, sLine.IndexOf(';')));
                                sLine = sLine.Remove(0, sLine.IndexOf(';') + 1);
                                itensHistoric[iCounter].colorItem = it.TakeColorByName(sLine.Substring(0, sLine.IndexOf(';')));
                                sLine = sLine.Remove(0, sLine.IndexOf(';') + 1);
                                itensHistoric[iCounter].typeItem = sLine.Substring(0, sLine.IndexOf(';'));
                                sLine = sLine.Remove(0, sLine.IndexOf(';') + 1);
                                itensHistoric[iCounter].quantityItem = int.Parse(sLine.Substring(0, sLine.IndexOf(';')));
                                sLine = sLine.Remove(0, sLine.IndexOf(';') + 1);
                                itensHistoric[iCounter].percentDiscountItem = double.Parse(sLine.Substring(0, sLine.IndexOf(';')));
                                sLine = sLine.Remove(0, sLine.IndexOf(';') + 1);
                                itensHistoric[iCounter].valueDiscountItem = double.Parse(sLine.Substring(0, sLine.IndexOf(';')));
                                sLine = sLine.Remove(0, sLine.IndexOf(';') + 1);
                                itensHistoric[iCounter].totalItem = double.Parse(sLine.Substring(0, sLine.IndexOf(';')));
                                sLine = sLine.Remove(0, sLine.IndexOf(';') + 1);
                                itensHistoric[iCounter].datePurchase = sLine.Substring(0, sLine.IndexOf(';'));
                                sLine = sLine.Remove(0, sLine.IndexOf(';') + 1);
                                iCounter++;
                                //call the method that realocate how many itens can be inside the object itenshistoric
                                itensHistoric = utils.Redim(itensHistoric, true, iCounter + 1);
                            }
                        }
                        //close the file
                        file.Close();
                        //call the method to show results
                        iLastLine = ShowResults(ref itensHistoric, ref User, false);
                        CreateBorders(iLastLine);

                    }
                    bValidate = false;
                }
                //if user input same option of buying item
                else if (itens[iNumberItem].item != Items.eTypeOfItem.checkout)
                {
                    //call method that show title of item
                    TitleItemScreen(ref itens, iNumberItem);
                    //call method that write the questions about item
                    MakeQuestions(itens, iNumberItem);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(LEFTPOSITIONSCREEN, 16);
                    //question user if user likes to buy another item
                    Console.Write("Do you want to buy another item? [Y]es OR [N]o. [ ]");
                    Console.SetCursorPosition(51, 16);
                    sAnswerBuyNewItem = Console.ReadLine();
                    //loop to validate that user just press y or n
                    while (sAnswerBuyNewItem.ToUpper() != "Y" && sAnswerBuyNewItem.ToUpper() != "N")
                    {
                        MesssageAlertScreen("Please input [Y]es OR [N]o.");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN, 16);
                        Console.Write("Do you want to buy another item? [Y]es OR [N]o. [ ]");
                        Console.SetCursorPosition(51, 16);
                        sAnswerBuyNewItem = Console.ReadLine();
                    }
                    //if user press Y continue purchansing
                    if (sAnswerBuyNewItem.ToUpper() == "Y")
                    {
                        bValidate = true;
                        iNumberItem += 1;
                        //realocate quantity if positions in array
                        itens = utils.Redim(itens, true, iNumberItem + 1);
                    }
                    else
                    {
                        //if user try to left but no purchased more than 2 itens
                        if (itens.Length < 2)
                        {
                            MesssageAlertScreen("You have to buy at least 2 items!!!");
                            bValidate = true;
                            iNumberItem += 1;
                            itens = utils.Redim(itens, true, iNumberItem + 1);
                        }
                        //if user buyed more than 2 itens but in order don't have shirt
                        else if (itens.Length >= 2 && !FindIfBuyingItem(ref itens, Items.eTypeOfItem.shirt))
                        {
                            MesssageAlertScreen("You need to buy a shirt.");
                            bValidate = true;
                            iNumberItem += 1;
                            itens = utils.Redim(itens, true, iNumberItem + 1);
                        }
                        else if (itens.Length >= 2)
                        {
                            iLastLine = 0;
                            iLastLine = ShowResults(ref itens, ref User, true);
                            bValidate = false;
                        }
                    }
                }
            }
            return iLastLine;
        }

        //Method that search user inside file user.txt
        public bool FindIfBuyingItem(ref Items.Itens[] itens, Items.eTypeOfItem item)
        {
            int iCount = 0;
            //loop that run in the ohjects and validate if object is the same and have quantity
            foreach (Items.Itens items in itens)
            {
                if (items.item == item && items.quantityItem > 0)
                    iCount += 1;
            }
            if (iCount >= 1)
                return true;
            else
                return false;
        }

        //method that show typeitem
        public void ReturnTypeItem(ref Items.Itens[] itens, int i)
        {
            int iReturnTypeItem = 0;
            //call method that clear title screen
            ClearTitleItemScreen();
            //call method that clear make question
            ClearMakeQuestionsScreen();
            //call method that show itens
            iReturnTypeItem = ItensScreen();
            //validate what user choice
            itens[i].item = iReturnTypeItem == 1 ? Items.eTypeOfItem.shirt :
              iReturnTypeItem == 2 ? Items.eTypeOfItem.pant :
              iReturnTypeItem == 3 ? Items.eTypeOfItem.hat :
              iReturnTypeItem == 4 ? Items.eTypeOfItem.sneakers :
              iReturnTypeItem == 5 ? Items.eTypeOfItem.skirt :
              iReturnTypeItem == 6 ? Items.eTypeOfItem.shorts :
              iReturnTypeItem == 7 ? Items.eTypeOfItem.checkout :
              iReturnTypeItem == 8 ? Items.eTypeOfItem.historic : Items.eTypeOfItem.exit;

        }

        //method that write the results in screen
        //this method is used for historic and when user finish the current purchase
        //the signature of method ask about what object itens, object users and if need save file of item (when is a new purchase input true)
        public int ShowResults(ref Items.Itens[] itens, ref Users.User[] user, bool bSaveFile)
        {
            //variables that control information in screen and file item.txt
            int j = 0;
            int iCount = 0;
            int iCountOrder = 1;
            int iLineOrder = 8;
            string sTypeItem = string.Empty;
            string sUser = user[0].username;
            string sGuidOrder = Guid.NewGuid().ToString();
            Utils util = new Utils();
            Items it = new Items();
            StringBuilder sb = new StringBuilder();

            //Print a summary report that shows what was bought including the type of shirt, colour of shirt, and total cost payable.
            ClearTitleItemScreen();
            ClearMakeQuestionsScreen();
            TitleUserScreen(ref user, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN + 30, 5);
            Console.Write("{0}Order Summary Report", bSaveFile ? "" : "Historic ");
            Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, 6);
            Console.Write("+========================================================================+");
            Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, 7);
            Console.Write("| Item     | Type of Item |Color of Item|Quantity| % Discount | Total    |");
            Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, 8);
            Console.Write("+========================================================================+");

            //if option to save file means that need create ou add information about itens
            if (bSaveFile)
            {
                j = 9;
                //call method that calculate order
                CalculeOrder(ref itens, ref user, false);
                if (!util.CheckFileExist(util.FileItem))
                    util.CreateFile(util.FileItem);
                foreach (Items.Itens item in itens)
                {
                    if (item.quantityItem > 0)
                    {

                        sTypeItem = it.TakeSubTypeItem(item);

                        Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, j++);
                        Console.Write("|{0, -10}|{1, -14}|{2, -13}|{3, 8}|{4, 12}|{5, 10}|",
                            it.TakeTypeOfItem(item.item),
                            sTypeItem,
                            it.TakeColor(item.colorItem),
                            item.quantityItem,
                            item.percentDiscountItem.ToString("0.00"),
                            item.totalItem.ToString("0.00"));
                    }

                    //add one line about itens is purchasing
                    sb.AppendLine(user[0].username + ";" + sGuidOrder + ";" + it.TakeTypeOfItem(item.item) + ";" + it.TakeColor(item.colorItem) + ";" +
                        it.TakeSubTypeItem(item) + ";" + item.quantityItem.ToString() + ";" + item.percentDiscountItem.ToString("0.00") + ";" +
                        item.valueDiscountItem.ToString("0.00") + ";" +
                    item.totalItem.ToString("0.00") + ";" + item.datePurchase + ";");
                }
                Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, j++);
                Console.Write("+========================================================================+");
                Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, j++);
                Console.Write("Total Order: {0}", TotalOrder.ToString("0.00"));
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, j++);
                Console.Write("Total Order + TAX (%13.00): {0}", TotalOrderPlusTax.ToString("0.00"));

                //assignment 3 where needs ask about crazy lottery, show the information in the result
                if (DiscountRandom > 0 && AnswerCrazyLottery.ToUpper() == "Y")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, j++);
                    Console.Write("Crazy Lottery Discount: {0}", DiscountRandom.ToString("0.00"));
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, j++);
                Console.Write("+========================================================================+");
                util.WriteFile(util.FileItem, sb);
            }
            //if don't need to save, just need to show informations in the screen
            else
            {
                //this variable use to group information about historic of purchase of specific user
                var guid = from x in itens
                           where x.userPurchase == sUser
                           select x.guidOrder;
                //this variable take just diferente purchases, because user can bought more than 1 purchase per day
                var result = guid.Distinct();

                //set variable
                iLineOrder = 8;
                //loop to run for show diferents orders
                foreach (string sGuid in result)
                {
                    if (sGuid != "")
                    {
                        Items.Itens[] itensHistoric = new Items.Itens[1];
                        iCount = 0;
                        //loop to take itens and change dimension of array of itens.
                        foreach (Items.Itens item in itens.Where(c => c.userPurchase == sUser).Where(c => c.guidOrder == sGuid))
                        {
                            itensHistoric[iCount] = item;
                            iCount++;
                            itensHistoric = util.Redim(itensHistoric, true, iCount + 1);
                        }
                        //call method to calculate total order
                        CalculeOrder(ref itensHistoric, ref user, true);

                        Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, iLineOrder++);
                        Console.Write("+========================================================================+");
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, iLineOrder++);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Order #{0}", iCountOrder);
                        iCountOrder++;
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, iLineOrder++);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+========================================================================+");
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, iLineOrder++);
                        Console.Write("Date of Purchase: {0}", Convert.ToDateTime(itensHistoric[0].datePurchase).ToString("MM/dd/yyyy"));
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, iLineOrder++);
                        Console.Write("+========================================================================+");

                        //loop to show each item of order
                        foreach (Items.Itens item in itensHistoric)
                        {
                            if (item.quantityItem > 0)
                            {
                                Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, iLineOrder++);
                                Console.Write("|{0, -10}|{1, -14}|{2, -13}|{3, 8}|{4, 12}|{5, 10}|",
                                    it.TakeTypeOfItem(item.item),
                                    item.typeItem,
                                    it.TakeColor(item.colorItem),
                                    item.quantityItem,
                                    item.percentDiscountItem.ToString("0.00"),
                                    item.totalItem.ToString("0.00"));
                            }
                        }
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, iLineOrder++);
                        Console.Write("+========================================================================+");
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, iLineOrder++);
                        Console.Write("Total Order: {0}", TotalOrder.ToString("0.00"));
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, iLineOrder++);
                        Console.Write("Total Order + TAX (%13.00): {0}", TotalOrderPlusTax.ToString("0.00"));

                        //assingment 3 - if discount more than 15 is because have a random discount
                        if (DiscountRandom > 15)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, iLineOrder++);
                            Console.Write("Crazy Lottery Discount: {0}", DiscountRandom.ToString("0.00"));
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, iLineOrder++);
                        Console.Write("+========================================================================+");
                    }
                }
            }
            return bSaveFile ? j : iLineOrder;
        }

        //mehtod that clear title screen
        public void ClearTitleItemScreen()
        {
            ReDrawLine(5);
            ReDrawLine(6);
        }

        //method that ask user about what item user like to buy
        public int ItensScreen()
        {
            bool bValidate = false;
            int iInputAnswer = 0;
            int iLine = 5;

            Items it = new Items();

            //loop to validate that user press just number of option to buy
            while (!bValidate)
            {
                iLine = 5;
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, iLine);
                Console.Write("Type a option: [ ]");
                for (int i = 1; i <= Items.eTypeOfItem.exit.GetHashCode(); i++)
                {
                    iLine += 1;
                    Console.SetCursorPosition(LEFTPOSITIONSCREEN + 15, iLine);
                    Console.Write("[{0}] - {1}", i, (i >= Items.eTypeOfItem.shirt.GetHashCode() && i <= Items.eTypeOfItem.shorts.GetHashCode() ?
                        "Buy - " + it.TakeTypeOfItemByNumber(i) :
                        i == Items.eTypeOfItem.checkout.GetHashCode() ? "Checkout" :
                        i == Items.eTypeOfItem.historic.GetHashCode() ? "Purchases historic" : "Exit"));
                }
                Console.SetCursorPosition(18, 5);
                InputAnswer = Console.ReadLine();
                //validation if user press number or letter diferent of that exists
                if (!int.TryParse(InputAnswer, out iInputAnswer))
                {
                    MesssageAlertScreen("Please select one of the available options = 1 to " + Items.eTypeOfItem.exit.GetHashCode() + "!!!");
                }
                else if (int.Parse(InputAnswer) > Items.eTypeOfItem.exit.GetHashCode())
                {
                    MesssageAlertScreen("Please select one of the available options = 1 to " + Items.eTypeOfItem.exit.GetHashCode() + "!!!");
                }
                else if (int.Parse(InputAnswer) == Items.eTypeOfItem.historic.GetHashCode())
                {
                    MesssageAlertScreen("");
                    bValidate = true;
                }
                else if (int.Parse(InputAnswer) == Items.eTypeOfItem.exit.GetHashCode())
                {
                    MesssageAlertScreen("Thank you for coming!!! Press ENTER to exit.");
                    Console.ReadLine();
                    Environment.Exit(1);
                }
                else
                {
                    MesssageAlertScreen("");
                    bValidate = true;
                }
            }

            iLine = 5;
            for (int i = 1; i <= Items.eTypeOfItem.exit.GetHashCode() + 1; i++)
            {
                ReDrawLine(iLine);
                iLine += 1;
            }
            return int.Parse(InputAnswer);
        }

        //mehtod that clear screen about questions
        public void ClearMakeQuestionsScreen()
        {
            ReDrawLine(7);
            ReDrawLine(8);
            ReDrawLine(9);
            ReDrawLine(10);
            ReDrawLine(11);
            ReDrawLine(12);
            ReDrawLine(13);
            ReDrawLine(14);
            ReDrawLine(15);
        }

        //method that show the questions for user in the screen
        public void MakeQuestions(Items.Itens[] itens, int iNumberItem)
        {
            //declaration of variables 
            bool bValidate; // this variable is used to control question about quantity of itens
            string sQuantityItem = string.Empty;
            int iQuantityItem = 0;
            int iInputAnswer = 0;

            bValidate = false;
            Items it = new Items();

            //Prompt for and then receive the input from the user needed to write a program to solve the above
            while (!bValidate)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 7);
                Console.Write("What colour of {0} you want to buy? [ ]", it.TakeTypeOfItem(itens[iNumberItem].item));

                Console.SetCursorPosition(35 + it.TakeTypeOfItem(itens[iNumberItem].item).Length, 8);
                Console.Write("[1] - Red");
                Console.SetCursorPosition(35 + it.TakeTypeOfItem(itens[iNumberItem].item).Length, 9);
                Console.Write("[2] - Blue");
                Console.SetCursorPosition(35 + it.TakeTypeOfItem(itens[iNumberItem].item).Length, 10);
                Console.Write("[3] - Green");
                Console.SetCursorPosition(35 + it.TakeTypeOfItem(itens[iNumberItem].item).Length, 11);
                Console.Write("[4] - Yellow");
                Console.SetCursorPosition(35 + it.TakeTypeOfItem(itens[iNumberItem].item).Length, 12);
                Console.Write("[5] - White");
                Console.SetCursorPosition(35 + it.TakeTypeOfItem(itens[iNumberItem].item).Length, 13);
                Console.Write("[6] - Black");
                Console.SetCursorPosition(36 + it.TakeTypeOfItem(itens[iNumberItem].item).Length, 7);
                InputAnswer = Console.ReadLine();

                if (!int.TryParse(InputAnswer, out iInputAnswer))
                {
                    MesssageAlertScreen("Please select one of the available options = 1 to 6 !!!");
                }
                else if (int.Parse(InputAnswer) > 6)
                {
                    MesssageAlertScreen("Please select one of the available options = 1 to 6 !!!");
                }
                else
                {
                    MesssageAlertScreen("");
                    bValidate = true;
                }
            }
            bValidate = false;
            MesssageAlertScreen("");
            iInputAnswer = int.Parse(InputAnswer);
            itens[iNumberItem].colorItem = iInputAnswer == 1 ? Items.eColor.red :
              iInputAnswer == 2 ? Items.eColor.blue :
              iInputAnswer == 3 ? Items.eColor.green :
              iInputAnswer == 4 ? Items.eColor.yellow :
              iInputAnswer == 5 ? Items.eColor.white :
              Items.eColor.black;

            //jusk ask about type 
            if (itens[iNumberItem].item == Items.eTypeOfItem.shirt ||
                itens[iNumberItem].item == Items.eTypeOfItem.pant ||
                itens[iNumberItem].item == Items.eTypeOfItem.skirt ||
                itens[iNumberItem].item == Items.eTypeOfItem.sneakers ||
                itens[iNumberItem].item == Items.eTypeOfItem.shorts)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 14);
                Console.Write("What is the type of {0}? ([1] {1} OR [2] {2}) [ ]", it.TakeTypeOfItem(itens[iNumberItem].item),
                  itens[iNumberItem].item == Items.eTypeOfItem.shirt ? "long sleeve" : "big size   ",
                  itens[iNumberItem].item == Items.eTypeOfItem.shirt ? "short sleeve" : "small size  ");
                Console.SetCursorPosition(63 + it.TakeTypeOfItem(itens[iNumberItem].item).Length, 14);
                itens[iNumberItem].typeItem = Console.ReadLine();
                //loop for control to user input just 1 or 2
                while (itens[iNumberItem].typeItem != "1" && itens[iNumberItem].typeItem != "2")
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Please type  (Press [1] for {0} OR [2] for {1})",
                      itens[iNumberItem].item == Items.eTypeOfItem.shirt ? "long sleeve" : "big size   ",
                      itens[iNumberItem].item == Items.eTypeOfItem.shirt ? "short sleeve" : "small size  ");
                    MesssageAlertScreen(sb.ToString());
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(LEFTPOSITIONSCREEN, 8);
                    Console.Write("What is the type of {0}? ([1] {1} OR [2] {2}) [ ]", it.TakeTypeOfItem(itens[iNumberItem].item),
                    itens[iNumberItem].item == Items.eTypeOfItem.shirt ? "long sleeve" : "big size   ",
                    itens[iNumberItem].item == Items.eTypeOfItem.shirt ? "short sleeve" : "small size  ");
                    Console.SetCursorPosition(63 + it.TakeTypeOfItem(itens[iNumberItem].item).Length, 14);
                    itens[iNumberItem].typeItem = Console.ReadLine();
                }
            }
            MesssageAlertScreen("");
            // ask question until user input numbers
            while (!bValidate)
            {
                ReDrawLine(itens[iNumberItem].item == Items.eTypeOfItem.shirt ||
                  itens[iNumberItem].item == Items.eTypeOfItem.pant ||
                  itens[iNumberItem].item == Items.eTypeOfItem.skirt ||
                  itens[iNumberItem].item == Items.eTypeOfItem.sneakers ||
                  itens[iNumberItem].item == Items.eTypeOfItem.shorts ? 15 : 14);
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, itens[iNumberItem].item == Items.eTypeOfItem.shirt ||
                  itens[iNumberItem].item == Items.eTypeOfItem.pant ||
                  itens[iNumberItem].item == Items.eTypeOfItem.skirt ||
                  itens[iNumberItem].item == Items.eTypeOfItem.sneakers ||
                  itens[iNumberItem].item == Items.eTypeOfItem.shorts ? 15 : 14);
                Console.Write("How many {0} you want to buy? ", it.TakeTypeOfItem(itens[iNumberItem].item));
                Console.SetCursorPosition(29 + it.TakeTypeOfItem(itens[iNumberItem].item).Length, itens[iNumberItem].item == Items.eTypeOfItem.shirt ||
                  itens[iNumberItem].item == Items.eTypeOfItem.pant ||
                  itens[iNumberItem].item == Items.eTypeOfItem.skirt ||
                  itens[iNumberItem].item == Items.eTypeOfItem.sneakers ||
                  itens[iNumberItem].item == Items.eTypeOfItem.shorts ? 15 : 14);
                sQuantityItem = Console.ReadLine();
                if (int.TryParse(sQuantityItem, out iQuantityItem))
                {
                    if (int.Parse(sQuantityItem) > 0 && int.Parse(sQuantityItem) <= 10)
                        bValidate = true;
                    else if (int.Parse(sQuantityItem) <= 0)
                    {
                        MesssageAlertScreen("Please type integer positive numbers!!!");
                    }
                    else if (int.Parse(sQuantityItem) > 10)
                    {
                        MesssageAlertScreen("Sorry!! You can not buy more than 10 items!!!");
                    }
                }
                else
                {
                    MesssageAlertScreen("Please type only numbers!!!");
                }
            }


            MesssageAlertScreen("");
            itens[iNumberItem].quantityItem = int.Parse(sQuantityItem);
            itens[iNumberItem].datePurchase = DateTime.Today.ToString(); ;
        }

        //method that show message alert in the screen
        public void MesssageAlertScreen(string sMessage)
        {
            ReDrawLine(37);
            ReDrawLine(38);
            if (sMessage != string.Empty)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 37);
                Console.Write("----------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 38);
                Console.Write(sMessage);
                Console.SetCursorPosition(0, 0);
                Console.Beep(5000, 500);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 37);
                Console.Write("                                                                            ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 38);
                Console.Write(sMessage);
                Console.SetCursorPosition(0, 0);
            }
            Console.SetCursorPosition(0, 0);
        }

        //method that re draw the line
        public void ReDrawLine(int iLine)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i <= 78; i++)
            {
                Console.SetCursorPosition(i, iLine);
                Console.Write(" ");
            }
            Console.SetCursorPosition(0, iLine);
            Console.Write("||");
            Console.SetCursorPosition(78, iLine);
            Console.Write("||");

        }

        //method that create borders of screen
        public void CreateBorders(int iLastLine)
        {
            Console.ForegroundColor = ConsoleColor.White;
            //Console.SetWindowSize(80, 40);
            int iValidateLine = (iLastLine > 39) ? iLastLine : 39;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetWindowSize(80, 40);

            for (int i = 0; i <= 78; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("=");
                Console.SetCursorPosition(i, (iLastLine > 39) ? iLastLine : 39);
                Console.Write("=");

            }
            for (int i = 0; i <= iValidateLine; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("||");
                Console.SetCursorPosition(78, i);
                Console.Write("||");
}
        }

        //method that calculate total order
        public void CalculeOrder(ref Items.Itens[] itens, ref Users.User[] user, bool bCalculeHistoric)
        {
            //variable that take a aleatory number.
            Random rRandom = new Random();
            int xResultRandom;
            Items it = new Items();

            //assingment 3
            /*
              40% chance of a 5% discount
              30% chance of a 10% discount
              15% chance of a 20% discount
              10% chance of a 50% discount
              5% chance of a 75% discount

             */

            //can be use foreach with a object itens, but I will need create 2 objects just to change information
            if (bCalculeHistoric)
            {
                //calculate value total order 
                foreach (Items.Itens item in itens)
                {
                    TotalOrder += item.totalItem;
                }
            }
            else
            {
                xResultRandom = rRandom.Next(1, 100); // random number from 1 to 100
                if (xResultRandom >= 1 && xResultRandom <= 40)
                    DiscountRandom = 5;
                else if (xResultRandom >= 41 && xResultRandom <= 70)
                    DiscountRandom = 10;
                else if (xResultRandom >= 71 && xResultRandom <= 85)
                    DiscountRandom = 20;
                else if (xResultRandom >= 86 && xResultRandom <= 95)
                    DiscountRandom = 50;
                else if (xResultRandom >= 96 && xResultRandom <= 100)
                    DiscountRandom = 75;

                for (int i = 0; i < itens.Length; i = i + 1)
                {
                    if (itens[i].quantityItem > 0)
                    {
                        if (itens[i].quantityItem >= 3)
                            itens[i].percentDiscountItem = 15.0;
                        else if (user[0].studentsenior == 1)
                            itens[i].percentDiscountItem = 10.0;
                        else
                            itens[i].percentDiscountItem = 0.0;

                        if (itens[i].percentDiscountItem < DiscountRandom && AnswerCrazyLottery.ToUpper() == "Y")
                            itens[i].percentDiscountItem = DiscountRandom;

                        //calcule to cost of total of item
                        itens[i].valueDiscountItem = ((itens[i].quantityItem * it.TakePriceOfItem(itens[i].item)) * itens[i].percentDiscountItem) / 100;

                        //calcule to cost of total of item
                        itens[i].totalItem = (itens[i].quantityItem * it.TakePriceOfItem(itens[i].item)) - itens[i].valueDiscountItem;

                        //calculate value total order 
                        TotalOrder += itens[i].totalItem;

                    }
                }
            }

            //calculate value total order + tax
            TotalOrderPlusTax = ((TotalOrder * SALETAX) / 100) + TotalOrder;
        }

        #endregion
    }

    //this class contains constants, variable, methods and functions that be utilities for the program
    class Utils
    {
        #region Constants and Variables

        const string FILEUSER = "users.txt"; //this constant define the name of file the users
        const string FILEITEM = "items.txt"; //this constant define the name of file the items
        private string sPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString(); //this variable define the name of directory where the program run;
        public string Path
        {
            get
            {
                return sPath;
            }
            set
            {
                sPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString(); //this variable define the name of directory where the program run
            }
        }

        private string sFileUser = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\" + FILEUSER; //this variable define the full name of directory and file of users;
        public string FileUser
        {
            get
            {
                return sFileUser;
            }
            set
            {
                sFileUser = Path + "\\" + FILEUSER; //this variable define the full name of directory and file of users
            }
        }
        private string sFileItem = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\" + FILEITEM; //this variable define the full name of directory and file of items;
        public string FileItem
        {
            get
            {
                return sFileItem;
            }
            set
            {
                sFileItem = Path + "\\" + FILEITEM; //this variable define the full name of directory and file of items
            }
        }
        #endregion

        #region Methods and Functions 
        //method that change dimension of arrays
        public T[] Redim<T>(T[] arr, bool preserved, int nbRows)
        {
            T[] arrRedimed = new T[nbRows];
            if (preserved)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    arrRedimed[i] = arr[i];
                }
            }
            return arrRedimed;
        }

        //method that verify if exist the file
        public bool CheckFileExist(string sFile)
        {

            return System.IO.File.Exists(sFile);

        }

        //method that create the file
        public void CreateFile(string sFile)
        {
            try
            {
                StreamWriter writer;
                writer = new StreamWriter(sFile);
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        }

        //method that read the file
        public StringBuilder ReadFile(string sFile)
        {
            StringBuilder sb = new StringBuilder();
            StreamReader reader;
            reader = new StreamReader(sFile);
            while (reader.EndOfStream == false)
            {
                sb.AppendLine(reader.ReadLine());
            }
            reader.Close();
            return sb;
        }

        //method that write in the file
        public void WriteFile(string sFile, StringBuilder sbNew)
        {
            StreamWriter writer;
            StringBuilder sb = new StringBuilder();


            try
            {
                if (CheckFileExist(sFile))
                {
                    sb = ReadFile(sFile);
                    File.Delete(sFile);
                    writer = new StreamWriter(sFile);
                    writer.Write(sb.ToString());
                    writer.Write(sbNew.ToString());
                }
                else
                {
                    writer = new StreamWriter(sFile);
                    writer.Write(sbNew.ToString());
                }
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

        }

        #endregion

    }

    //class that control everything about User
    class Users
    {

        #region Declaration of Structs

        //structure of user
        public struct User
        {
            public string username;
            public string fullname;
            public string bday;
            public string pwd;
            public int studentsenior;
        }

        #endregion

        #region Declaration of Enumerators

        //enumerator of type user
        public enum eTypeUser
        {
            currentuser = 1,
            newtuser = 2
        }
        #endregion

        #region Methods and Functions Validate User

        //method that find user in file
        public User[] FindUser(string sUserName)
        {
            //create a object of class utils
            Utils util = new Utils();
            int counter = 0;
            string line;
            //create a object of structure user
            User[] user = new User[1];

            //save file user
            if (!util.CheckFileExist(util.FileUser))
            {
                user[0].username = "";
                user[0].fullname = "";
                user[0].bday = "";
                user[0].pwd = "";
                user[0].studentsenior = 0;
            }
            else
            {
                //if file exists needs verify if user exists in file
                // Read the file and display it line by line.  
                System.IO.StreamReader file = new System.IO.StreamReader(util.FileUser);
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Substring(0, line.IndexOf(';')) == sUserName)
                    {

                        user[0].username = line.Substring(0, line.IndexOf(';'));
                        line = line.Remove(0, line.IndexOf(';') + 1);
                        user[0].fullname = line.Substring(0, line.IndexOf(';'));
                        line = line.Remove(0, line.IndexOf(';') + 1);
                        user[0].bday = line.Substring(0, line.IndexOf(';'));
                        line = line.Remove(0, line.IndexOf(';') + 1);
                        user[0].pwd = line.Substring(0, line.IndexOf(';'));
                        line = line.Remove(0, line.IndexOf(';') + 1);
                        user[0].studentsenior = int.Parse(line.Substring(0, line.IndexOf(';')));
                        continue;
                    }
                    counter++;
                }
                //close the file
                file.Close();
            }

            return user;
        }

        //method that validate if user and password are correct
        public bool ValidUserAndPassword(ref Users.User[] user, string sPWD)
        {
            if (sPWD == user[0].pwd)
                return true;
            else return false;
        }
        #endregion

    }

    //class responsable to mantain items
    class Items
    {
        #region Declaration of Constants

        //constants utilized to save price of itens
        const double SHIRTCOST = 7.99;
        const double PANTCOST = 10.50;
        const double HATCOST = 3.00;
        const double SNEAKERSCOST = 15.70;
        const double SKIRTCOST = 13.25;
        const double SHORTSCOST = 8.00;

        #endregion

        #region Declaration of Structs

        //structure of itens
        public struct Itens
        {
            public string guidOrder;
            public eTypeOfItem item;
            public eColor colorItem;
            public string typeItem;
            public int quantityItem;
            public double percentDiscountItem;
            public double valueDiscountItem;
            public double totalItem;
            public string datePurchase;
            public string userPurchase;
        }

        #endregion

        #region Declaration of Enumerators

        //enumerator of itens
        public enum eTypeOfItem
        {
            shirt = 1,
            pant = 2,
            hat = 3,
            sneakers = 4,
            skirt = 5,
            shorts = 6,
            checkout = 7,
            historic = 8,
            exit = 9
        }

        //enumerator of colors of itens
        public enum eColor
        {
            red = 1,
            blue = 2,
            green = 3,
            yellow = 4,
            white = 5,
            black = 6
        }

        #endregion

        #region Methods And Functions

        //method that return enumerator of color
        public eColor TakeColorByName(string sColor)
        {
            switch (sColor.ToUpper())
            {
                case "RED":
                    {
                        return eColor.red;

                    }
                case "BLUE":
                    {
                        return eColor.blue;
                    }
                case "GREEN":
                    {
                        return eColor.green;
                    }
                case "YELLOW":
                    {
                        return eColor.yellow;
                    }
                case "WHITE":
                    {
                        return eColor.white;
                    }
                case "BLACK":
                    {
                        return eColor.black;
                    }
                default:
                    return eColor.black;
            }
        }

        //method that return string name of color
        public string TakeColor(eColor color)
        {
            switch (color)
            {
                case eColor.red:
                    {
                        return "Red";

                    }
                case eColor.blue:
                    {
                        return "Blue";
                    }
                case eColor.green:
                    {
                        return "Green";
                    }
                case eColor.yellow:
                    {
                        return "Yellow";
                    }
                case eColor.white:
                    {
                        return "White";
                    }
                case eColor.black:
                    {
                        return "Black";
                    }
                default:
                    return "";
            }
        }

        //mehtod that return string of type of item
        public string TakeSubTypeItem(Items.Itens item)
        {
            string sTypeItem;
            if (item.item == Items.eTypeOfItem.shirt)
            {
                sTypeItem = item.typeItem == "1" ? "Long sleeve" : "Short sleeve";
            }
            else if (item.item == Items.eTypeOfItem.pant ||
               item.item == Items.eTypeOfItem.shorts ||
               item.item == Items.eTypeOfItem.skirt ||
               item.item == Items.eTypeOfItem.sneakers)
            {
                sTypeItem = item.typeItem == "1" ? "Big size   " : "Small size  ";
            }
            else
                sTypeItem = string.Empty;
            return sTypeItem;
        }

        //method that return price of item 
        public double TakePriceOfItem(eTypeOfItem type)
        {
            switch (type)
            {
                case eTypeOfItem.shirt:
                    {
                        return SHIRTCOST;

                    }
                case eTypeOfItem.pant:
                    {
                        return PANTCOST;
                    }
                case eTypeOfItem.hat:
                    {
                        return HATCOST;
                    }
                case eTypeOfItem.sneakers:
                    {
                        return SNEAKERSCOST;
                    }
                case eTypeOfItem.skirt:
                    {
                        return SKIRTCOST;
                    }
                case eTypeOfItem.shorts:
                    {
                        return SHORTSCOST;
                    }

                default:
                    return 0.0;
            }
        }

        //method that return the name of item
        public string TakeTypeOfItemByNumber(int type)
        {
            switch (type)
            {
                case 1:// eTypeOfItem.shirt.GetHashCode():
                    {
                        return "Shirt(s)";

                    }
                case 2:// eTypeOfItem.pant.GetHashCode():
                    {
                        return "Pants";
                    }
                case 3:// eTypeOfItem.hat.GetHashCode():
                    {
                        return "Hat(s)";
                    }
                case 4:// eTypeOfItem.sneakers.GetHashCode():
                    {
                        return "Sneakers";
                    }
                case 5:// eTypeOfItem.skirt.GetHashCode():
                    {
                        return "Skirt(s)";
                    }
                case 6:// eTypeOfItem.shorts.GetHashCode():
                    {
                        return "Shorts";
                    }

                default:
                    return "";
            }
        }

        //method that return enumerator of item 
        public eTypeOfItem TakeTypeOfItemByName(string sType)
        {
            switch (sType)
            {
                case "Shirt":
                    {
                        return eTypeOfItem.shirt;

                    }
                case "Pant":
                    {
                        return eTypeOfItem.pant;
                    }
                case "Hat":
                    {
                        return eTypeOfItem.hat;
                    }
                case "Sneakers":
                    {
                        return eTypeOfItem.sneakers;
                    }
                case "Skirt":
                    {
                        return eTypeOfItem.skirt;
                    }
                case "Shorts":
                    {
                        return eTypeOfItem.shorts;
                    }

                default:
                    return eTypeOfItem.checkout;
            }
        }

        //method that return name of item 
        public string TakeTypeOfItem(eTypeOfItem type)
        {
            switch (type)
            {
                case eTypeOfItem.shirt:
                    {
                        return "Shirt";

                    }
                case eTypeOfItem.pant:
                    {
                        return "Pant";
                    }
                case eTypeOfItem.hat:
                    {
                        return "Hat";
                    }
                case eTypeOfItem.sneakers:
                    {
                        return "Sneakers";
                    }
                case eTypeOfItem.skirt:
                    {
                        return "Skirt";
                    }
                case eTypeOfItem.shorts:
                    {
                        return "Shorts";
                    }

                default:
                    return "";
            }
        }
        #endregion

    }
}
