using System.Text.RegularExpressions;

namespace AdventOfCode._2020 {
    public class Day4 : IAdventDay {
        string input;

        public Day4() {
            this.input = Helper.GetInputOneLine(4);
        }

        public long PartOne() {
            int validPasswords = 0;
            Dictionary<string, bool> fieldIds = new Dictionary<string, bool>(){
                {"byr", true},
                {"iyr", true},
                {"eyr", true},
                {"hgt", true},
                {"hcl", true},
                {"ecl", true},
                {"pid", true},
                {"cid", false}
            };

            int expectedRequiredFieldCount = fieldIds.Count(x => x.Value);
            string[] passports = input.Replace("\r", "").Replace("\n\n", "@").Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string passport in passports) {
                string[] parts = passport.Split(new char[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var fields = parts.Select(x => x.Split(':')).Select(x => new { key = x[0], value = x[1] }).ToList();
                int requiredFieldCount = 0;
                foreach (var field in fields) {
                    if (fieldIds.TryGetValue(field.key, out bool required)) {
                        if (required) ++requiredFieldCount;
                    }
                }
                if (requiredFieldCount == expectedRequiredFieldCount) {
                    ++validPasswords;
                }
            }
            return validPasswords;
        }

        public long PartTwo() {
            int validPasswords = 0;
            Func<string, int, int, bool> fourDigitsValue = (val, min, max) => !string.IsNullOrWhiteSpace(val) && val.Length == 4 && int.TryParse(val, out int year) && year >= min && year <= max;
            Func<string, bool> birthYearCheck = (val) => fourDigitsValue(val, 1920, 2002);
            Func<string, bool> issueYearCheck = (val) => fourDigitsValue(val, 2010, 2020);
            Func<string, bool> expirationYearCheck = (val) => fourDigitsValue(val, 2020, 2030);
            Func<string, bool> heightCheck = (val) => {
                var match = new Regex(@"^(?<val>\d+)(?<kind>in|cm)$").Match(val);
                if (match.Success) {
                    if (!int.TryParse(match.Groups["val"].Value, out int length)) return false;

                    string kind = match.Groups["kind"].Value;
                    switch (kind) {
                        case "in": {
                            return length >= 59 && length <= 76;
                        }
                        case "cm": {
                            return length >= 150 && length <= 193;
                        }
                        default:
                            return false;
                    }
                }
                return false;
            };
            Func<string, string, bool> regexPattern = (val, pattern) => new Regex(pattern).Match(val).Success;
            Func<string, bool> hairColorCheck = (val) => regexPattern(val, "^#[0-9a-f]{6}$");
            Func<string, bool> eyeColorCheck = (val) => regexPattern(val, "^(amb|blu|grn|brn|gry|hzl|oth)$");
            Func<string, bool> pidCheck = (val) => regexPattern(val, "^[0-9]{9}$");

            Dictionary<string, (bool required, Func<string, bool> validator)> fieldIds = new Dictionary<string, (bool required, Func<string, bool> validator)>() {
                {"byr", (true, birthYearCheck)},
                {"iyr", (true, issueYearCheck)},
                {"eyr", (true, expirationYearCheck)},
                {"hgt", (true, heightCheck)},
                {"hcl", (true, hairColorCheck)},
                {"ecl", (true, eyeColorCheck)},
                {"pid", (true, pidCheck)},
                //{"cid", (false, null)}
            };

            int expectedRequiredFieldCount = fieldIds.Count(x => x.Value.required);
            string[] passports = input.Replace("\r", "").Replace("\n\n", "@").Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string passport in passports) {
                string[] parts = passport.Split(new char[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var fields = parts.Select(x => x.Split(':')).Select(x => new { key = x[0], value = x[1] }).ToList();
                int requiredFieldCount = 0;
                foreach (var field in fields) {
                    if (fieldIds.TryGetValue(field.key, out (bool required, Func<string, bool> validator) info)) {
                        if (info.required && info.validator(field.value)) ++requiredFieldCount;
                    }
                }
                if (requiredFieldCount == expectedRequiredFieldCount) {
                    ++validPasswords;
                }
            }
            return validPasswords;
        }
    }
}
