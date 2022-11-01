using SendGrid.Helpers.Mail;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace SpellingCheckDictLibrary
{
    public class SpellingCheck
    {
        int count = 0;
        public string SentenceSpellCheck(string sentence,DataTable dt)
        {            
            try
            {
                string SentenceChecked = null;
                foreach (var part in sentence.Trim().Split(' ').Distinct())
                {                   
                    var temp = dt.AsEnumerable().Select(s => s.Field<string>("English-Word")).Any(s => s.ToLower().Contains(part.ToLower()));
                    if (temp == true)
                    {   
                        SentenceChecked += " " + part;
                    }
                    else
                    {
                        if (part.Where(c => char.IsNumber(c)).Any() || part.Contains('.')|| part.Contains(',') || part.Contains('-') || part.Contains('!') || part.Contains(':') || part.Contains('/') || part.Contains('(') || part.Contains(')') || part.Contains('`') || part.Contains('?') || part.Contains("'"))
                        {
                            if(part.Contains('.') || part.Contains(',') || part.Contains('-') || part.Contains('!') || part.Contains(':') || part.Contains('/') || part.Contains('(') || part.Contains(')') || part.Contains('`') || part.Contains('?') || part.Contains("'"))
                            {
                                SentenceChecked += " "+part;
                            }
                            else
                            {
                                SentenceChecked += " " + part;
                            }
                            
                        }
                        else
                        {
                             count++;
                             var WrongWord = "<b class='text-danger'>" + part + "</b>";
                             SentenceChecked += " " + WrongWord;
                        }
                    }


                }
                
                return SentenceChecked.Trim();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CountWrong()
        {           
            return count;
        }
    }

   
}
