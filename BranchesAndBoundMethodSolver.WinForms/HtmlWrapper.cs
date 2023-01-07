using System.Globalization;
using System.Text;
using BranchesAndBoundMethodSolver.Logic;

namespace BranchesAndBoundMethodSolver.WinForms
{
    public class HtmlWrapper
    {
        public static string Wrapp(IEnumerable<Node> nodes)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append("<table>\n" +
                " <thead>\n" +
                "     <tr>\n" +
                "         <th scope=\"col\"><span style=\"\">Підмножина</span></th>\n" +
                "         <th scope=\"col\"><span style=\"\">Оцінка</span></th>\n" +
                "         <th scope=\"col\"><span style=\"\">Статус</span></th>\n" +
                "     </tr>\n" +
                " </thead>\n" +
                " <tbody>\n");

            foreach (var node in nodes)
            {
                stringBuilder.Append("     <tr>\r\n" +
                    "         <td>{1:SA:=" + $"{node.Path}" + "}</td>\n" +
                    "         <td>{1:SA:=" + $"{node.Cost}" + "}</td>\n" +
                    "         <td>{1:MC:=розгалужена~виключена&nbsp;за ВД~виключена&nbsp;за тестом~рекорд~-}</td>\n" +
                    "     </tr>");
            }

            string endOfHtml = " </tbody>\n" +
                "</table>";

            stringBuilder.Append(endOfHtml);

            return stringBuilder.ToString();
        }
    }
}
