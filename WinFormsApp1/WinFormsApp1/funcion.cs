using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class funcion
    {
        /*1*/
        private List<string> keywords = new List<string>() { "var", "begin", "end", "readln", "writeln", "if", "then", "else", "while", "true", "false", "or", "not", "end.", "program", "for", "to", "dim", "next" };

        /*0program | 1var | 2begin | 3end | 4readln | 5writeln |  6if | 7then | 8else | 9while  * | 10true | 11false |12 or | 13not*/

        /*2*/
        private List<string> separators = new List<string>() { ",", ":", ";", ":=", ".", "(", ")", "~", "+", "-", "<", ">", "<=", ">=", "<>", "*", "/", "!", "$", "%", "=" };

        /*1     2    3    4    5     6    7     8   9    10   11   12   13   14   15    16    17    18   19*/

        private List<string> keys = new List<string>();
        private List<string> numbers = new List<string>();
        private List<string> variebles = new List<string>();

        string S = "";/* буфер*/
        char CH; /* очередной входной символ */
        string CS; /* текущее состояние буфера накопления лексем*/
        int Num = 0;
        int Num_keys = 0;
        string LEX;
        char znak;
        string CL;
        char CL2;
        string p;

        public List<string> Keys { get => keys; set => keys = value; }
        public List<string> Keywords { get => keywords; set => keywords = value; }
        public List<string> Separators { get => separators; set => separators = value; }
        public List<string> Numbers { get => numbers; set => numbers = value; }
        public List<string> Variebles { get => variebles; set => variebles = value; }

        public funcion()
        {

        }

        #region sintactical_analyzer
        bool let()
        {
            char b = char.Parse(CH.ToString());
            if (Char.IsLetter(b))
            {
                if (Regex.IsMatch(CH.ToString(), "[a-zA-Z]"))
                    return true;
                else return false;
            }
            else return false;
        }
        bool digit()
        {
            int n;
            bool isNumeric = int.TryParse(CH.ToString(), out n);
            return isNumeric;
        }
        void nill()
        {
            S = "";
        }
        void add()
        {
            S = S + CH;
            Num++;
        }
        void gc(string richTextBox1)
        {
            CH = richTextBox1[Num];
        }
        int search_in_keywords()
        {
            if (keywords.BinarySearch(S) > -1)
            {
                return keywords.BinarySearch(S);
            }
            else
            {
                return -1;
            }

        }
        int search_in_separators()
        {
            if (separators.BinarySearch(S) > -1)
            {
                return separators.BinarySearch(S);
            }
            else
            {
                return -1;
            }
        }
        bool check_for_latin()
        {
            if ((CH >= 'А' && CH >= 'Я') || (CH >= 'а' && CH >= 'я'))
            {
                Num++;
                return true;
            }
            else
            {
                return false;
            }

        }
        string convertation(int system)
        {
            try
            {
                return (Convert.ToInt32(S, system).ToString());
            }
            catch
            {
                return ("!");
            }
        }
        string convertatuon_to_decimal()
        {
            S = S.Replace('.', ',');
            try
            {
                return (double.Parse(S).ToString());
            }
            catch
            {
                return "Не корректное значение";
            }
        }
        #endregion


        public string sintactical_analyzer(string richtextbox1)
        {
            Num = 0;
            CS = "H";
            if (richtextbox1 == "")
            {
                CS = "ER";
                return "Ведите что-нибудь";
            }

            while (CS != "V" && CS != "ER")
            {
                gc(richtextbox1);
                switch (CS)
                {
                    case "H":
                        {
                            if (check_for_latin())
                            {
                                CS = "V";
                                break;
                            }
                            if (Num == richtextbox1.Length - 1)
                            {
                                CS = "V";
                                break;
                            }
                            if ((CH == ' ' || CH == '\n' || CH == '\t' || CH == '\0' || CH == '\r' && CH != '#') && (Num < richtextbox1.Length - 1))
                            {
                                Num++;
                                gc(richtextbox1);
                                break;
                            }
                            else if (let())
                            {
                                nill();
                                add();
                                gc(richtextbox1);
                                CS = "I";
                            }
                            else if (digit())
                            {
                                nill();
                                add();
                                gc(richtextbox1);
                                CS = "N";
                            }
                            else if (CH == '/')
                            {
                                nill();
                                add();
                                CS = "C1";
                                gc(richtextbox1);
                                if (CH == '*')
                                {
                                    add();
                                    nill();
                                    CS = "C2";
                                    gc(richtextbox1);
                                    while (CH != '*' && Num < richtextbox1.Length - 1)
                                    {
                                        Num++;
                                        gc(richtextbox1);
                                    }
                                    if (Num == richtextbox1.Length - 1)
                                    {
                                        CS = "ER";
                                        return "Комментарий не закрыт!";
                                    }
                                    else if (CH == '*')
                                    {
                                        add();
                                        gc(richtextbox1);
                                        if (CH == '/')
                                        {
                                            add();
                                        }
                                    }
                                    nill();
                                }
                                else
                                {
                                    keys.Add("(1, " + search_in_separators().ToString() + ")");
                                }
                                CS = "H";
                                break;
                            }
                            else if (CH == '.')
                            {
                                add();
                                gc(richtextbox1);
                                CS = "P1";
                            }
                            else if (CH == ':')
                            {
                                nill();
                                add();
                                gc(richtextbox1);
                                if (CH == '=')
                                {
                                    add();
                                    keys.Add("(1, " + search_in_separators().ToString() + ")");
                                }
                                else
                                {
                                    keys.Add("(1, " + search_in_separators().ToString() + ")");
                                }
                                nill();
                            }
                            else if (CH == '>')
                            {
                                nill();
                                add();
                                gc(richtextbox1);
                                if (CH == '=')
                                {
                                    add();
                                    keys.Add("(1, " + search_in_separators().ToString() + ")");
                                }
                                else
                                {
                                    keys.Add("(1, " + search_in_separators().ToString() + ")");
                                }
                                nill();
                            }
                            else if (CH == '<')
                            {
                                nill();
                                add();
                                gc(richtextbox1);
                                if (CH == '=')
                                {
                                    add();
                                    keys.Add("(1, " + search_in_separators().ToString() + ")");
                                }
                                else if (CH == '>')
                                {
                                    add();
                                    keys.Add("(1, " + search_in_separators().ToString() + ")");
                                }
                                else
                                {
                                    keys.Add("(1, " + search_in_separators().ToString() + ")");
                                    nill();
                                }
                            }
                            else
                            {
                                nill();
                                add();
                                gc(richtextbox1);
                                CS = "OG";
                            }
                            break;
                        }
                    case "I":
                        {
                            while ((let() || digit()) && Num != richtextbox1.Length - 1)
                            {
                                add();
                                gc(richtextbox1);
                            }
                            if (search_in_keywords() < 0)
                            {
                                if (search_in_separators() < 0)
                                {
                                    keys.Add("(4, " + variebles.Count().ToString() + ")");
                                    variebles.Add(S);
                                }
                                else
                                {
                                    keys.Add("(1, " + search_in_separators().ToString() + ")");
                                }
                            }
                            else
                            {
                                if (CH == '.' && search_in_keywords() == 2)
                                {
                                    add();
                                    gc(richtextbox1);
                                    keys.Add("(2, " + search_in_keywords().ToString() + ")");
                                    CS = "H";
                                    return "Программа завершена";
                                }
                                keys.Add("(2, " + search_in_keywords().ToString() + ")");
                            }
                            CS = "H";
                            break;
                        }
                    case "OG":
                        {
                            if ((search_in_separators() == 0 || search_in_separators() == 12) && digit())
                            {
                                znak = S[0];
                                S = S.Remove(0, 1);
                                CS = "N";
                                break;
                            }
                            else if (search_in_separators() > -1)
                            {
                                if (CH == '*' && Num < richtextbox1.Length - 1)
                                {
                                    add();
                                    gc(richtextbox1);
                                    if (CH == '/')
                                    {
                                        add();
                                        keys.Add("(1, " + search_in_separators().ToString() + ")");
                                    }
                                }
                                else
                                {
                                    keys.Add("(1, " + search_in_separators().ToString() + ")");
                                }
                            }
                            else
                            {
                                return "Неизвестная переменная";
                            }
                            CS = "H";
                            break;
                        }
                    case "N":
                        {
                            while ((let() || digit()) && Num != richtextbox1.Length - 1)
                            {
                                if (CH == 'E' || CH == 'e')
                                {
                                    add();
                                    gc(richtextbox1);
                                    CS = "E22";
                                    break;
                                }
                                add();
                                gc(richtextbox1);
                            }
                            if (CS == "E22")
                            {
                                break;
                            }
                            if (CH == '.')
                            {
                                add();
                                gc(richtextbox1);
                                CS = "P1";
                                break;
                            }
                            Num = Num - 1;
                            gc(richtextbox1);
                            S = S.Remove(S.Length - 1, 1);
                            if (CH == 'B' || CH == 'b')
                            {
                                CS = "N2";
                                numbers.Add(znak + convertation(2));
                                keys.Add("(3, " + (numbers.Count() - 1).ToString() + ")");
                            }
                            else if (CH == 'O' || CH == 'o')
                            {
                                CS = "N8";
                                numbers.Add(znak + convertation(8));
                                keys.Add("(3, " + (numbers.Count() - 1).ToString() + ")");
                            }
                            else if (CH == 'D' || CH == 'd')
                            {
                                CS = "N10";
                                numbers.Add(znak + convertation(10));
                                keys.Add("(3, " + (numbers.Count() - 1).ToString() + ")");
                            }
                            else if (CH == 'H' || CH == 'h')
                            {
                                CS = "N16";
                                numbers.Add(znak + convertation(16));
                                keys.Add("(3, " + (numbers.Count() - 1).ToString() + ")");
                            }
                            else
                            {
                                return "Неправильно введено число/1";
                            }
                            Num = Num + 1;
                            CS = "H";
                            break;
                        }
                    case "P1":
                        {
                            if (digit())
                            {
                                CS = "P2";
                            }
                            else
                            {
                                CS = "ER";
                                return "Неправильно введено число/2";
                            }
                            break;
                        }
                    case "P2":
                        {
                            while (digit())
                            {
                                add();
                                gc(richtextbox1);
                            }
                            if (CH == 'E' || CH == 'e')
                            {
                                add();
                                gc(richtextbox1);
                                CS = "E21";
                            }
                            else if (let() || CH == '.')
                            {
                                CS = "ER";
                            }
                            else
                            {
                                keys.Add("(3, " + numbers.Count().ToString() + ")");
                                numbers.Add(convertatuon_to_decimal());
                                CS = "H";
                            }
                            break;
                        }
                    case "E21":
                        {
                            if (CH == '+' || CH == '-')
                            {
                                add();
                                gc(richtextbox1);
                            }
                            else if (digit())
                            {
                                CS = "E22";
                            }
                            else
                            {
                                CS = "ER";
                            }
                            break;
                        }
                    case "E22":
                        {
                            while (digit())
                            {
                                add();
                                gc(richtextbox1);
                            }
                            if (CH == 'H' || CH == 'h')
                            {
                                keys.Add("(3, " + numbers.Count().ToString() + ")");
                                numbers.Add(convertation(16));
                                CS = "H";
                                return S;
                            }
                            else if (let() || CH == '.')
                            {
                                CS = "ER";
                            }
                            else
                            {
                                keys.Add("(3, " + numbers.Count().ToString() + ")");
                                numbers.Add(convertatuon_to_decimal());
                                CS = "H";
                                nill();
                            }
                            break;
                        }
                }
            }
            return "Успешно";
        }

    }
}
