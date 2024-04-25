using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using System.ComponentModel;


namespace Sparta_Dungeon
{
    internal class Program
    {
        static bool start = true;
        static bool key;
        static int k = 1;

        static void Main(string[] args)
        {

            while (start == true) //시작화면
            {
                Console.Clear();

                if (k < 0)
                { Console.WriteLine("잘못된 입력입니다."); k *= -1; }

                Console.WriteLine("\n스파르타 마을에 오신 여러분 환영합니다.\n" +
                "이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n\n" +
                "1. 상태 보기\n" + "2. 인벤토리\n" + "3. 상점\n\n" +
                "원하시는 행동을 입력해주세요.");


                string playerChoice = Console.ReadLine();
                int numChoice;
                bool strChoice = int.TryParse(playerChoice, out numChoice);

                if (strChoice == true)
                {
                    if (numChoice < 1 || numChoice >= 5)
                    { k *= -1; }
                    else
                    {
                        switch (numChoice)
                        {
                            case 1:
                                Status();
                                break;

                            case 2:
                                Inventory();
                                break;

                            case 3:
                                Shop();
                                break;                        

                        }
                    } //선택에 따른 함수
                }
                else
                { k *= -1; }

            }



        }


        static string[] statusComponent = { "Lv.", "Rtan", "( 전사 )", "공격력", "방어력", "체력", "Gold" };
        static int[] statusValue = { 01, 10, 5, 100, 1500 };

        static void Status()
        {
            Console.Clear();

            if (k < 0)
            { Console.WriteLine("잘못된 입력입니다."); k *= -1; }

            Console.WriteLine("\n상태 보기\n캐릭터의 정보가 표시됩니다.\n" +
                $"\n{statusComponent[0]}  {statusValue[0]}" +
                $"\n{statusComponent[1]}  {statusComponent[2]}" +
                $"\n{statusComponent[3]} : {statusValue[1]}" +
                $"\n{statusComponent[4]} : {statusValue[2]}" +
                $"\n{statusComponent[5]} : {statusValue[3]}" +
                $"\n{statusComponent[6]}   {statusValue[4]}" +
                "\n\n0. 나가기\n\n" + "원하시는 행동을 입력해주세요.");

            string answer = Console.ReadLine();
            int numAnswer;
            bool strAnswer = int.TryParse(answer, out numAnswer);
            key = true;

            while (key == true)
            {
                if (strAnswer == false)
                {
                    k *= -1; ;
                    Status();
                }
                else
                {
                    if (numAnswer == 0)
                    {
                        key = false;
                        return;
                    }
                    else
                    {
                        k *= -1;
                        Status();
                    }
                }

            }
        }

        //아이템의 경우 6개의 데이터 i번호 [ ]장착확인 이름 방어력/공격력 설명 가격 x 배열을 여러개 기능도 메서드로 다쪼개
        //static string[] itemInfo = {i 장착확인 이름 방어력/공격력 설명 가격}
        //무기 키값 4 5 6   방어구 키값 1 2 3 매칭  있으면 출력하는 변수와 메서드 weaponKey gearKey

        public class ItemInfo
        {
            public int inShop; //0 인벤토리, 1 장착관리, 2 상점 
            public int[] equip = { -1, -1, -1, -1, -1, -1 }; //-1 미장착, 1 장착            
            string[] equipment = { "[]", "[]", "[]", "[]", "[]", "[]" };
            int itemType; //0 무기, 1 방어구


            //index
            int[] indexArray = { 1, 2, 3, 4, 5, 6 };
            //itemName
            string[] itemNameArray = { "수련자 갑옷", "무쇠갑옷", "스파르타의 갑옷", "낡은 검", "청동 도끼", "스파르타의 창" };
            //atkValue
            int[] atkValueArray = { 0, 0, 0, 2, 5, 7 };
            //defValue
            int[] defValueArray = { 5, 9, 15, 0, 0, 0 };
            //info
            string[] infoArray = {
                "수련에 도움을 주는 갑옷입니다.",
                "무쇠로 만들어져 튼튼한 갑옷입니다.",
                "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
                "쉽게 볼 수 있는 낡은 검 입니다.",
                "어디선가 사용됐던거 같은 도끼입니다.",
                "스파르타의 전사들이 사용했다는 전설의 창입니다."
            };
            //price
            public int[] priceArray = { 1000, 2000, 3500, 600, 1500, 2800 };



            public int n;
            void EquipmentCheck()
            {

                if (equip[n] == 1)  //-1 미장착, 1 장착
                {
                    switch (n)
                    {
                        case 1:
                            statusValue[2] += 5;
                            break;
                        case 2:
                            statusValue[2] += 9;
                            break;
                        case 3:
                            statusValue[2] += 15;
                            break;
                        case 4:
                            statusValue[1] += 2;
                            break;
                        case 5:
                            statusValue[1] += 5;
                            break;
                        case 6:
                            statusValue[1] += 7;
                            break;
                    }
                    equipment[n] = "[E]";
                }
                else
                {
                    switch (n)
                    {
                        case 1:
                            statusValue[2] -= 5;
                            break;
                        case 2:
                            statusValue[2] -= 9;
                            break;
                        case 3:
                            statusValue[2] -= 15;
                            break;
                        case 4:
                            statusValue[1] -= 2;
                            break;
                        case 5:
                            statusValue[1] -= 5;
                            break;
                        case 6:
                            statusValue[1] -= 7;
                            break;
                    }
                }
                equipment[n] = " ";
            }


            public void itemInfoPrint(int i)
            {

                for (i = 0; i < 6; i++)
                {
                    EquipmentCheck();
                    if (inShop == 0)//인벤토리
                    {
                        if (i > 2)
                        { Console.WriteLine($"- {equipment}|{itemNameArray[i]}\t| 공격력 +{atkValueArray[i]} | {infoArray[i]}"); }
                        else
                        { Console.WriteLine($"- {equipment}|{itemNameArray[i]}\t| 방어력 +{defValueArray[i]} | {infoArray[i]}"); }
                    }
                    else if (inShop == 1)//장착관리
                    {

                        if (i > 2)
                        { Console.WriteLine($"- {indexArray[i]} {equipment}{itemNameArray[i]}\t| 공격력 +{atkValueArray[i]} | {infoArray[i]}"); }
                        else
                        { Console.WriteLine($"- {indexArray[i]} {equipment}{itemNameArray[i]}\t| 방어력 +{defValueArray[i]} | {infoArray[i]}"); }

                    }
                    else
                    {
                        if (i > 2)
                        { Console.WriteLine($"- {indexArray[i]} {itemNameArray[i]}\t| 공격력 +{atkValueArray[i]} | {infoArray[i]} | {priceArray[i]}G"); }
                        else
                        { Console.WriteLine($"- {indexArray[i]} {itemNameArray[i]}\t| 방어력 +{defValueArray[i]} | {infoArray[i]} | {priceArray[i]}G"); }
                    }
                }
            }
        }


        static void Inventory()
        {
            Console.Clear();

            if (k < 0)
            { Console.WriteLine("잘못된 입력입니다."); k *= -1; }

            ItemInfo info = new ItemInfo();

            Console.WriteLine("\n인벤토리" + "\n보유 중인 아이템을 관리할 수 있습니다." + "\n[아이템 목록]\n");

            info.inShop = 0;
            info.itemInfoPrint(0);

            Console.WriteLine("\n\n0. 나가기\n" + "1. 장착 관리\n\n\"" + "원하시는 행동을 입력해주세요.");

            string answer = Console.ReadLine();
            int numAnswer;
            bool strAnswer = int.TryParse(answer, out numAnswer);
            key = true;

            while (key == true)
            {
                if (strAnswer == false)
                {
                    k *= -1;
                    Inventory();
                }
                else
                {
                    if (numAnswer == 0)
                    {
                        key = false;
                        return;
                    }
                    else if (numAnswer == 1)
                    {
                        equipCheck();
                    }

                    else
                    {
                        k *= -1;
                        Inventory();
                    }

                }
            }

            void equipCheck()
            {
                Console.Clear();

                if (k < 0)
                { Console.WriteLine("잘못된 입력입니다."); k *= -1; }

                ItemInfo info = new ItemInfo();

                Console.WriteLine("\n장착 관리" + "\n보유 중인 아이템을 착용할 수 있습니다." + "\n[아이템 목록]\n");

                info.inShop = 1;
                info.itemInfoPrint(0);

                Console.WriteLine("\n\n0. 나가기\n\n" + "원하시는 행동을 입력해주세요.");

                string answer1 = Console.ReadLine();
                int numAnswer1;
                bool strAnswer1 = int.TryParse(answer1, out numAnswer1);
                bool key1 = true;

                while (key1 == true)
                {
                    if (strAnswer == false)
                    {
                        k *= -1;
                        equipCheck();
                    }
                    else //statusValue[] 1-공 2-방
                    {
                        info.n = numAnswer1 - 1;
                        switch (numAnswer1)
                        {
                            case 1:
                                info.equip[0] *= -1; key1 = false;
                                break;
                            case 2:
                                info.equip[1] *= -1; key1 = false;
                                break;
                            case 3:
                                info.equip[2] *= -1; key1 = false;
                                break;
                            case 4:
                                info.equip[3] *= -1; key1 = false;
                                break;
                            case 5:
                                info.equip[4] *= -1; key1 = false;
                                break;
                            case 6:
                                info.equip[5] *= -1; key1 = false;
                                break;
                            case 0:
                                key1 = false; Inventory();
                                break;
                            default:
                                k *= -1; info.n = 0;
                                equipCheck();
                                break;

                        }
                    }
                }

            }

        }

        static void Shop()
        {
            Console.Clear();

            if (k < 0)
            { Console.WriteLine("잘못된 입력입니다."); k *= -1; }

            ItemInfo info = new ItemInfo();

            Console.WriteLine("\n상점" + "\n필요한 아이템을 얻을 수 있는 상점입니다." + "\n\n[보유 골드]\n" + statusValue[4] + "\n\n[아이템 목록]");

            info.inShop = 2;
            info.itemInfoPrint(0);

            Console.WriteLine("\n\n0. 나가기\n" + "1. 아이템 구매\n\n\"" + "원하시는 행동을 입력해주세요.");

            

            string answer = Console.ReadLine();
            int numAnswer;
            bool strAnswer = int.TryParse(answer, out numAnswer);
            key = true;

            while (key == true)
            {
                if (strAnswer == false)
                {
                    k *= -1;
                    Shop();
                }
                else
                {
                    if (numAnswer == 0)
                    {
                        key = false;
                        return;
                    }
                    else if (numAnswer == 1)
                    {
                        Console.WriteLine("구매하실 아이템을 선택해주세요.");
                        string answer2 = Console.ReadLine();
                        int numAnswer2;
                        bool strAnswer2 = int.TryParse(answer2, out numAnswer2);
                        if (numAnswer2 < 0 | numAnswer2 > 6)
                        { k *= -1; }
                        else
                        { Pay(numAnswer2 - 1);}
                        Shop();
                    }

                    else
                    {
                        k *= -1;
                        Shop();
                    }

                }
            }


           int Pay(int m)
            {
                int change;

                if (statusValue[4] < info.priceArray[m])
                {
                    Console.WriteLine("Gold가 부족합니다.");
                    return statusValue[4];
                }
                else
                {
                    Console.WriteLine("아이템을 구매했습니다.");
                    change = statusValue[4] - info.priceArray[m];
                    return statusValue[4] = change;
                }
            }

        }
        

        /*statusValue[2] += 5;
        statusValue[2] += 9;
        statusValue[2] += 15;
        statusValue[1] += 2;
        statusValue[1] += 5;
        statusValue[1] += 7;*/


    }
}