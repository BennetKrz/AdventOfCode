using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2016 {
    public class Day5 : IAdventDay {
        string input = "ojvtpuvg";

        public Day5() {

        }

        public long PartOne() {
            MD5 md5 = MD5.Create();
            string password = string.Empty;
            int counter = 0;
            while(password.Length < 8) {
                string toHash = input + counter.ToString();
                byte[] bytes = Encoding.ASCII.GetBytes(toHash);
                byte[] result = md5.ComputeHash(bytes);
                string hash = Convert.ToHexString(result);
                if(hash.Substring(0, 5) == "00000") {
#if DEBUG
                    Console.WriteLine(hash);
#endif
                    password += hash[5];
                }
                counter++;
            }
            Console.WriteLine(password);
            return long.MinValue;
        }

        public long PartTwo() {
            MD5 md5 = MD5.Create();
            char[] password = new char[8];
            int counter = 0;
            int digits = 0;
            while(digits < 8) {
                string toHash = input + counter.ToString();
                byte[] bytes = Encoding.ASCII.GetBytes(toHash);
                byte[] result = md5.ComputeHash(bytes);
                string hash = Convert.ToHexString(result);
                if(hash.Substring(0, 5) == "00000") {
#if DEBUG
                    Console.WriteLine(hash);
#endif
                    char position = hash[5];
                    int pos;
                    if(char.IsDigit(position)) {
                        pos = int.Parse(position.ToString());
                        if(pos > 7) {
                            counter++;
                            continue;
                        }
                        if(password[pos] != '\0') {
                            counter++;
                            continue;
                        }
                        password[pos] = hash[6];
                        digits++;
                    }
                }
                counter++;
            }
            Console.WriteLine(password);
            return long.MinValue;
        }
    }
}
