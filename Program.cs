using System;

namespace ClotheSale
{
    class Program
    {

        #region Declaration of Constants

        const int LEFTPOSITIONSCREEN = 2;
        const double SALETAX = 13.00;

        #endregion

        #region Declaration of Variables

        public static string sInputAnswer;
        public static double dTotalOrder = 0;
        public static double dTotalOrderPlusTax = 0;

        #endregion

        #region Methods and Functios

        static void Main(string[] args)
        {
            bool bContinue = false;
            string sYesNo = "Y";

            Console.Title = "Clothes Sale";

            while (sYesNo.ToUpper() == "Y")
            {
                Console.Clear();
                CreateBorders();
                WelcomeScreen();
                QuestionUserScreen();

                Users.eTypeUser typeUser = int.Parse(sInputAnswer) == Users.eTypeUser.currentuser.GetHashCode() ?
                  Users.eTypeUser.currentuser : Users.eTypeUser.newtuser;

                Users.User[] User = new Users.User[1];

                switch (typeUser)
                {
                    case Users.eTypeUser.currentuser:
                        {
                            bContinue = LoginUserScreen(ref User);
                            break;
                        }
                    case Users.eTypeUser.newtuser:
                        {
                            bContinue = NewUserScreen(ref User);
                            break;
                        }
                    default:
                        break;
                }

                if (bContinue)
                {
                    ProcessQuestions(ref User);
                }

                ReDrawLine(35);
                ReDrawLine(36);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 35);
                Console.Write("Would you like to order another order?");
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 36);
                Console.Write("Press Y (es) for new order OR press ANY key to finish!");
                sYesNo = Console.ReadLine().ToUpper();
            }
            //Console.ReadLine();
        }

        public static void WelcomeScreen()
        {
            Console.SetCursorPosition(20, 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Welcome to the Clothing Sales System");
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

        public static void TitleSystemScreen()
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

        public static void QuestionUserScreen()
        {
            sInputAnswer = "?";

            while (sInputAnswer != "1" && sInputAnswer != "2")
            {
                ReDrawLine(6);
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 6);
                Console.Write("Press [1] for Register Users OR [2] for New User: [ ]");
                Console.SetCursorPosition(53, 6);
                sInputAnswer = Console.ReadLine();
                if (sInputAnswer != "1" && sInputAnswer != "2")
                    MesssageAlertScreen("Please Press [1] for Register Users OR [2] for New User.");
                else
                    MesssageAlertScreen("");
                Console.SetCursorPosition(53, 6);
            }
            Console.SetCursorPosition(0, 0);
        }

        public static bool LoginUserScreen(ref Users.User[] user)
        {
            bool bFindUser = false;
            bool bValidPwd = false;
            int iTemp = 1;
            Users users = new Users();

            Console.Clear();
            CreateBorders();
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
                sInputAnswer = Console.ReadLine();
                user = users.FindUser(sInputAnswer);
                bFindUser = user[0].username != string.Empty;
                if (bFindUser)
                {
                    MesssageAlertScreen("");
                    iTemp = 2;
                }
                else
                {
                    if (iTemp == 2)
                    {
                        MesssageAlertScreen("Login attempt exceeded. System will finish!!!");
                    }
                    else
                        MesssageAlertScreen("User do not find, please input a valid user name!");
                }
            }

            if (bFindUser)
            {
                for (iTemp = 0; iTemp <= 2; iTemp++)
                {
                    ReDrawLine(6);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(LEFTPOSITIONSCREEN, 6);
                    Console.Write("Password : ");
                    Console.SetCursorPosition(13, 6);
                    sInputAnswer = Console.ReadLine();
                    bValidPwd = users.ValidUserAndPassword(user[0].username, sInputAnswer);
                    if (bValidPwd)
                    {
                        MesssageAlertScreen("");
                        iTemp = 2;
                    }
                    else
                    {
                        if (iTemp == 2)
                        {
                            MesssageAlertScreen("Login attempt exceeded. System will finish!!!");
                        }
                        else
                            MesssageAlertScreen("Password invalid. Try Again!!!");
                    }
                }
            }
            if (bFindUser && bValidPwd)
                return true;
            else
                return false;
        }

        public static bool NewUserScreen(ref Users.User[] newUser)
        {
            bool bContinue = false;

            Users users = new Users();

            Console.Clear();
            CreateBorders();
            TitleSystemScreen();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(30, 3);
            Console.Write("New User Registration");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 4);
            Console.Write("----------------------------------------------------------------------------");
            sInputAnswer = QuestionUserName();
            while (!bContinue)
            {
                if (sInputAnswer.Length > 15)
                {
                    MesssageAlertScreen("User name should be less than 15 characters!!!");
                    ReDrawLine(5);
                    sInputAnswer = QuestionUserName();
                }
                else if (users.FindUser(sInputAnswer)[0].username != string.Empty)
                {
                    MesssageAlertScreen("User already exists!!!");
                    ReDrawLine(5);
                    sInputAnswer = QuestionUserName();
                }
                else if (sInputAnswer.IndexOf(" ") > 0)
                {
                    MesssageAlertScreen("Username can not have space!!!");
                    ReDrawLine(5);
                    sInputAnswer = QuestionUserName();
                }
                else
                {
                    MesssageAlertScreen("");
                    bContinue = true;
                    newUser[0].username = sInputAnswer;
                }
            }

            if (bContinue)
            {
                bContinue = false;
                sInputAnswer = QuestionFullName();

                while (!bContinue)
                {
                    if (sInputAnswer.Length > 40)
                    {
                        MesssageAlertScreen("Full name should be less than 40 characters!!!");
                        ReDrawLine(6);
                        sInputAnswer = QuestionFullName();
                    }
                    else
                    {
                        MesssageAlertScreen("");
                        bContinue = true;
                        newUser[0].fullname = sInputAnswer;
                    }
                }
            }

            if (bContinue)
            {
                bContinue = false;

                sInputAnswer = QuestionBday();
                while (!bContinue)
                {
                    DateTime dateValue;
                    if (DateTime.TryParse(sInputAnswer, out dateValue))
                    {
                        MesssageAlertScreen("Invalid date format!!! Format should be mm/dd/yyyy .");
                        ReDrawLine(7);
                        sInputAnswer = QuestionBday();
                    }
                    else
                    {
                        MesssageAlertScreen("");
                        bContinue = true;
                        newUser[0].bday = dateValue;
                    }
                }
            }

            if (bContinue)
            {
                bContinue = false;

                sInputAnswer = QuestionPassword();
                while (!bContinue)
                {
                    if (sInputAnswer.Length > 10)
                    {
                        MesssageAlertScreen("Password should be less than 10 characters!!!");
                        ReDrawLine(8);
                        sInputAnswer = QuestionPassword();
                    }
                    else if (sInputAnswer.Length < 3)
                    {
                        MesssageAlertScreen("Password should be more than 3 characters!!!");
                        ReDrawLine(8);
                        sInputAnswer = QuestionPassword();
                    }
                    else
                    {
                        MesssageAlertScreen("");
                        bContinue = true;
                        newUser[0].pwd = sInputAnswer;
                    }
                }
            }

            if (bContinue)
            {
                bContinue = false;
                sInputAnswer = QuestionRePassword();
                while (!bContinue)
                {
                    if (sInputAnswer != newUser[0].pwd)
                    {
                        MesssageAlertScreen("Password different from previous!!!");
                        ReDrawLine(9);
                        sInputAnswer = QuestionRePassword();
                    }
                    else
                    {
                        MesssageAlertScreen("");
                        bContinue = true;
                    }
                }
            }

            if (bContinue)
            {
                sInputAnswer = QuestionAboutSeniorOrStudent();

                while (sInputAnswer.ToUpper() != "Y" && sInputAnswer.ToUpper() != "N")
                {
                    MesssageAlertScreen("Please press [Y]es OR [N]o !!!");
                    ReDrawLine(10);
                    sInputAnswer = QuestionAboutSeniorOrStudent();
                }
                newUser[0].studentsenior = sInputAnswer.ToUpper() == "Y" ? 1 : 0;
                bContinue = true;
            }

            if (bContinue)
            {
                sInputAnswer = QuestionAboutPurchaseNow();

                while (sInputAnswer.ToUpper() != "Y" && sInputAnswer.ToUpper() != "N")
                {
                    MesssageAlertScreen("Please press [Y]es OR [N]o !!!");
                    ReDrawLine(11);
                    sInputAnswer = QuestionAboutPurchaseNow();
                }
            }
            bContinue = sInputAnswer.ToUpper() == "Y" ? true : false;
            return bContinue;
        }

        public static string QuestionUserName()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 5);
            Console.Write("User name: ");
            Console.SetCursorPosition(13, 5);
            return sInputAnswer;
        }

        public static string QuestionFullName()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 6);
            Console.Write("Full name: ");
            Console.SetCursorPosition(13, 6);
            return sInputAnswer;
        }

        public static string QuestionBday()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 7);
            Console.Write("Birthday : ");
            Console.SetCursorPosition(13, 7);
            return sInputAnswer;
        }

        public static string QuestionPassword()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 8);
            Console.Write("Password : ");
            Console.SetCursorPosition(13, 8);
            return sInputAnswer;
        }

        public static string QuestionRePassword()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 9);
            Console.Write("Retype Password : ");
            Console.SetCursorPosition(20, 9);

            return sInputAnswer;
        }

        public static string QuestionAboutSeniorOrStudent()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 10);
            Console.Write("Are you a student or a senior? [Y]es OR [N]o : [ ]");
            Console.SetCursorPosition(50, 10);
            return sInputAnswer;
        }

        public static string QuestionAboutPurchaseNow()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 11);
            Console.Write("Do you want to make a purchase now? [Y]es OR [N]o : [ ]");
            Console.SetCursorPosition(55, 11);
            sInputAnswer = Console.ReadLine();
            return sInputAnswer;

        }

        public static void TitleUserScreen(ref Users.User[] user)
        {
            Console.Clear();
            CreateBorders();
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

        public static void TitleItemScreen(ref Items.Itens[] item, int iNumberItem)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 5);
            Console.Write("You are buying: {0} - Price: ${1}", Items.TakeTypeOfItem(item[iNumberItem].item), Items.TakePriceOfItem(item[iNumberItem].item).ToString());
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN, 6);
            Console.Write("----------------------------------------------------------------------------");
            Console.SetCursorPosition(0, 0);
        }

        public static void ProcessQuestions(ref Users.User[] User)
        {
            int iQuantityItens = 1;
            int iNumberItem = 0;
            bool bValidate = true;
            string sAnswerBuyNewItem = "";

            Items.Itens[] itens = new Items.Itens[iQuantityItens];
            TitleUserScreen(ref User);
            Utils utils = new Utils();

            while (bValidate)
            {
                ReDrawLine(16);
                ReturnTypeItem(ref itens, iNumberItem);

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
                else if (itens.Length > 2 && itens[iNumberItem].item == Items.eTypeOfItem.checkout)
                {
                    ShowResults(ref itens, ref User);
                    bValidate = false;
                }
                else if (itens[iNumberItem].item != Items.eTypeOfItem.checkout)
                {

                    TitleItemScreen(ref itens, iNumberItem);
                    MakeQuestions(itens, iNumberItem);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(LEFTPOSITIONSCREEN, 16);
                    Console.Write("Do you want to buy another item? [Y]es OR [N]o. [ ]");
                    Console.SetCursorPosition(51, 16);
                    sAnswerBuyNewItem = Console.ReadLine();
                    while (sAnswerBuyNewItem.ToUpper() != "Y" && sAnswerBuyNewItem.ToUpper() != "N")
                    {
                        MesssageAlertScreen("Please input [Y]es OR [N]o.");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(LEFTPOSITIONSCREEN, 16);
                        Console.Write("Do you want to buy another item? [Y]es OR [N]o. [ ]");
                        Console.SetCursorPosition(51, 16);
                        sAnswerBuyNewItem = Console.ReadLine();
                    }
                    if (sAnswerBuyNewItem.ToUpper() == "Y")
                    {
                        bValidate = true;
                        iNumberItem += 1;

                        itens = utils.Redim(itens, true, iNumberItem + 1);
                    }
                    else
                    {
                        if (itens.Length < 2)
                        {
                            MesssageAlertScreen("You have to buy at least 2 items!!!");
                            bValidate = true;
                            iNumberItem += 1;
                            itens = utils.Redim(itens, true, iNumberItem + 1);
                        }
                        else if (itens.Length >= 2 && !FindIfBuyingItem(ref itens, Items.eTypeOfItem.shirt))
                        {
                            MesssageAlertScreen("You need to buy a shirt.");
                            bValidate = true;
                            iNumberItem += 1;
                            itens = utils.Redim(itens, true, iNumberItem + 1);
                        }
                        else if (itens.Length >= 2)
                        {
                            ShowResults(ref itens, ref User);
                            bValidate = false;
                        }
                    }
                }
            }
        }

        public static bool FindIfBuyingItem(ref Items.Itens[] itens, Items.eTypeOfItem item)
        {
            int iCount = 0;
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

        public static void ReturnTypeItem(ref Items.Itens[] itens, int i)
        {
            int iReturnTypeItem = 0;
            ClearTitleItemScreen();
            ClearMakeQuestionsScreen();
            iReturnTypeItem = ItensScreen();
            itens[i].item = iReturnTypeItem == 1 ? Items.eTypeOfItem.shirt :
              iReturnTypeItem == 2 ? Items.eTypeOfItem.pant :
              iReturnTypeItem == 3 ? Items.eTypeOfItem.hat :
              iReturnTypeItem == 4 ? Items.eTypeOfItem.sneakers :
              iReturnTypeItem == 5 ? Items.eTypeOfItem.skirt :
              iReturnTypeItem == 6 ? Items.eTypeOfItem.shorts :
              iReturnTypeItem == 7 ? Items.eTypeOfItem.checkout :
              iReturnTypeItem == 8 ? Items.eTypeOfItem.historic : Items.eTypeOfItem.exit;

        }

        public static void ShowResults(ref Items.Itens[] itens, ref Users.User[] user)
        {

            int j = 0;
            /*
            title lengths
            item.length 10
            Type.length 14
            Color.length 13
            qt.length 8
            discount.length 12
            total.length 10
             */

            CalculeOrder(ref itens, ref user);
            //Print a summary report that shows what was bought including the type of shirt, colour of shirt, and total cost payable.
            ClearTitleItemScreen();
            ClearMakeQuestionsScreen();
            TitleUserScreen(ref user);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN + 30, 5);
            Console.Write("Order Summary Report");
            Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, 6);
            Console.Write("+========================================================================+");
            Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, 7);
            Console.Write("| Item     | Type of Item |Color of Item|Quantity| % Discount | Total    |");
            Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, 8);
            Console.Write("+========================================================================+");

            j = 9;
            foreach (Items.Itens item in itens)
            {
                if (item.quantityItem > 0)
                {
                    Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, j);
                    Console.Write("|{0, -10}|{1, -14}|{2, -13}|{3, 8}|{4, 12}|{5, 10}|",
                        Items.TakeTypeOfItem(item.item),
                        (item.item == Items.eTypeOfItem.shirt) ? (item.typeItem == "1") ? "Long sleeve" : "Short sleeve" : "",
                        Items.TakeColor(item.colorItem),
                        item.quantityItem,
                        item.percentDiscountItem.ToString("0.00"),
                        item.totalItem.ToString("0.00"));
                }
                j += 1;
            }
            Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, j++);
            Console.Write("+========================================================================+");
            Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, j++);
            Console.Write("Total Order: {0}", dTotalOrder.ToString("0.00"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, j++);
            Console.Write("Total Order + TAX (%13.00): {0}", dTotalOrderPlusTax.ToString("0.00"));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(LEFTPOSITIONSCREEN + 1, j++);
            Console.Write("+========================================================================+");
        }

        public static void ClearTitleItemScreen()
        {
            ReDrawLine(5);
            ReDrawLine(6);
        }

        public static int ItensScreen()
        {
            bool bValidate = false;
            int iInputAnswer = 0;
            int iLine = 5;

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
                        "Buy - " + Items.TakeTypeOfItemByNumber(i) : 
                        i == Items.eTypeOfItem.checkout.GetHashCode() ? "Checkout" :
                        i == Items.eTypeOfItem.historic.GetHashCode() ? "Purchases historic" : "Exit"));
                }
                Console.SetCursorPosition(18, 5);
                sInputAnswer = Console.ReadLine();

                if (!int.TryParse(sInputAnswer, out iInputAnswer))
                {
                    MesssageAlertScreen("Please select one of the available options = 1 to " + Items.eTypeOfItem.exit.GetHashCode() + "!!!");
                }
                else if (int.Parse(sInputAnswer) > Items.eTypeOfItem.exit.GetHashCode())
                {
                    MesssageAlertScreen("Please select one of the available options = 1 to " + Items.eTypeOfItem.exit.GetHashCode() + "!!!");
                }
                else if (int.Parse(sInputAnswer) == Items.eTypeOfItem.historic.GetHashCode())
                {
                    MesssageAlertScreen("This function not implemented YET!!!");
                }
                else if (int.Parse(sInputAnswer) == Items.eTypeOfItem.exit.GetHashCode())
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
            return int.Parse(sInputAnswer);
        }

        public static void ClearMakeQuestionsScreen()
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

        public static void MakeQuestions(Items.Itens[] itens, int iNumberItem)
        {
            //declaration of variables 
            bool bValidate; // this variable is used to control question about quantity of itens
            string sQuantityItem = string.Empty;
            int iQuantityItem = 0;
            int iInputAnswer = 0;

            bValidate = false;

            //Prompt for and then receive the input from the user needed to write a program to solve the above
            while (!bValidate)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 7);
                Console.Write("What colour of {0} you want to buy? [ ]", Items.TakeTypeOfItem(itens[iNumberItem].item));

                Console.SetCursorPosition(35 + Items.TakeTypeOfItem(itens[iNumberItem].item).Length, 8);
                Console.Write("[1] - Red");
                Console.SetCursorPosition(35 + Items.TakeTypeOfItem(itens[iNumberItem].item).Length, 9);
                Console.Write("[2] - Blue");
                Console.SetCursorPosition(35 + Items.TakeTypeOfItem(itens[iNumberItem].item).Length, 10);
                Console.Write("[3] - Green");
                Console.SetCursorPosition(35 + Items.TakeTypeOfItem(itens[iNumberItem].item).Length, 11);
                Console.Write("[4] - Yellow");
                Console.SetCursorPosition(35 + Items.TakeTypeOfItem(itens[iNumberItem].item).Length, 12);
                Console.Write("[5] - White");
                Console.SetCursorPosition(35 + Items.TakeTypeOfItem(itens[iNumberItem].item).Length, 13);
                Console.Write("[6] - Black");
                Console.SetCursorPosition(36 + Items.TakeTypeOfItem(itens[iNumberItem].item).Length, 7);
                sInputAnswer = Console.ReadLine();

                if (!int.TryParse(sInputAnswer, out iInputAnswer))
                {
                    MesssageAlertScreen("Please select one of the available options = 1 to 6 !!!");
                }
                else if (int.Parse(sInputAnswer) > 6)
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
            iInputAnswer = int.Parse(sInputAnswer);
            itens[iNumberItem].colorItem = iInputAnswer == 1 ? Items.eColor.red :
              iInputAnswer == 2 ? Items.eColor.blue :
              iInputAnswer == 3 ? Items.eColor.green :
              iInputAnswer == 4 ? Items.eColor.yellow :
              iInputAnswer == 5 ? Items.eColor.white :
              Items.eColor.black;

            //jusk ask about type for shirts
            if (itens[iNumberItem].item == Items.eTypeOfItem.shirt)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, 14);
                Console.Write("What is the type of {0}? ([1] long sleeve OR [2] short sleeve) [ ]", Items.TakeTypeOfItem(itens[iNumberItem].item));
                Console.SetCursorPosition(63 + Items.TakeTypeOfItem(itens[iNumberItem].item).Length, 14);
                itens[iNumberItem].typeItem = Console.ReadLine();
                //loop for control to user input just 1 or 2
                while (itens[iNumberItem].typeItem != "1" && itens[iNumberItem].typeItem != "2")
                {
                    MesssageAlertScreen("Please type  (Press [1] for long sleeve OR [2] for short sleeve)");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(LEFTPOSITIONSCREEN, 8);
                    Console.Write("What is the type of {0}? ([1] long sleeve OR [2] short sleeve) [ ]", Items.TakeTypeOfItem(itens[iNumberItem].item));
                    Console.SetCursorPosition(63 + Items.TakeTypeOfItem(itens[iNumberItem].item).Length, 14);
                    itens[iNumberItem].typeItem = Console.ReadLine();
                }
            }
            MesssageAlertScreen("");
            // ask question until user input numbers
            while (!bValidate)
            {
                ReDrawLine(itens[iNumberItem].item == Items.eTypeOfItem.shirt ? 15 : 14);
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(LEFTPOSITIONSCREEN, itens[iNumberItem].item == Items.eTypeOfItem.shirt ? 15 : 14);
                Console.Write("How many {0} you want to buy? ", Items.TakeTypeOfItem(itens[iNumberItem].item));
                Console.SetCursorPosition(29 + Items.TakeTypeOfItem(itens[iNumberItem].item).Length, itens[iNumberItem].item == Items.eTypeOfItem.shirt ? 15 : 14);
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
        }

        public static void MesssageAlertScreen(string sMessage)
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

        public static void ReDrawLine(int iLine)
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

        public static void CreateBorders()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetWindowSize(80, 40);

            for (int i = 0; i <= 78; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("=");
                Console.SetCursorPosition(i, 39);
                Console.Write("=");

            }
            for (int i = 0; i <= 39; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("||");
                Console.SetCursorPosition(78, i);
                Console.Write("||");
            }
        }

        public static void CalculeOrder(ref Items.Itens[] itens, ref Users.User[] user)
        {
            //can be use foreach with a object itens, but I will need create 2 objects just to change information
            for (int i = 0; i < itens.Length; i = i + 1)
            {
                if (itens[i].quantityItem >= 3)
                    itens[i].percentDiscountItem = 15.0;
                else if (user[0].studentsenior == 1)
                    itens[i].percentDiscountItem = 10.0;
                else
                    itens[i].percentDiscountItem = 0.0;

                //calcule to cost of total of item
                itens[i].valueDiscountItem = ((itens[i].quantityItem * Items.TakePriceOfItem(itens[i].item)) * itens[i].percentDiscountItem) / 100;

                //calcule to cost of total of item
                itens[i].totalItem = (itens[i].quantityItem * Items.TakePriceOfItem(itens[i].item)) - itens[i].valueDiscountItem;

                //calculate value total order 
                dTotalOrder += itens[i].totalItem;
            }
            //calculate value total order + tax
            dTotalOrderPlusTax = ((dTotalOrder * SALETAX) / 100) + dTotalOrder;
        }

        #endregion
    }

    public class Utils
    {

        #region Methods and Functions 
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

        #endregion

    }

    public class Users
    {

        #region Declaration of Structs
        public struct User
        {
            public string username;
            public string fullname;
            public DateTime bday;
            public string pwd;
            public int studentsenior;
        }
        #endregion

        #region Declaration of Enumerators
        public enum eTypeUser
        {
            currentuser = 1,
            newtuser = 2
        }
        #endregion

        #region Methods and Functions Validate User

        public User[] FindUser(string sUserName)
        {
            User[] user = new User[1];
            if (sUserName.ToLower() == "danielbrazil")
            {
                user[0].username = "danielbrazil";
                user[0].fullname = "Daniel Brazil";
                user[0].bday = DateTime.MinValue;
                user[0].pwd = "123456";
                user[0].studentsenior = 1;
            }
            else
            {
                user[0].username = "";
                user[0].fullname = "";
                user[0].bday = DateTime.MinValue;
                user[0].pwd = "";
                user[0].studentsenior = 0;
            }
            return user;
        }

        public bool ValidUserAndPassword(string sUserName, string sPWD)
        {
            if (sUserName.ToLower() == "danielbrazil" && sPWD == "123456")
                return true;
            else return false;
        }
        #endregion

    }

    public class Items
    {
        #region Declaration of Constants

        const double SHIRTCOST = 7.99;
        const double PANTCOST = 10.50;
        const double HATCOST = 3.00;
        const double SNEAKERSCOST = 15.70;
        const double SKIRTCOST = 13.25;
        const double SHORTSCOST = 8.00;

        #endregion

        #region Declaration of Structs

        public struct Itens
        {
            public eTypeOfItem item;
            public eColor colorItem;
            public string typeItem;
            public int quantityItem;
            public double percentDiscountItem;
            public double valueDiscountItem;
            public double totalItem;
        }

        #endregion

        #region Declaration of Enumerators
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

        public static string TakeColor(eColor color)
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

        public static double TakePriceOfItem(eTypeOfItem type)
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

        public static string TakeTypeOfItemByNumber(int type)
        {
            switch (type)
            {
                case 1:// eTypeOfItem.shirt.GetHashCode():
                    {
                        return "Shirt";

                    }
                case 2:// eTypeOfItem.pant.GetHashCode():
                    {
                        return "Pant";
                    }
                case 3:// eTypeOfItem.hat.GetHashCode():
                    {
                        return "Hat";
                    }
                case 4:// eTypeOfItem.sneakers.GetHashCode():
                    {
                        return "Sneakers";
                    }
                case 5:// eTypeOfItem.skirt.GetHashCode():
                    {
                        return "Skirt";
                    }
                case 6:// eTypeOfItem.shorts.GetHashCode():
                    {
                        return "Shorts";
                    }

                default:
                    return "";
            }
        }

        public static string TakeTypeOfItem(eTypeOfItem type)
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
