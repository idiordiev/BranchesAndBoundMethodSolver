using System.Text;
using BranchesAndBoundMethodSolver.Logic.Enums;
using BranchesAndBoundMethodSolver.Logic.Models;

namespace BranchesAndBoundMethodSolver.WinForms.Helpers
{
    public class HtmlWrapper
    {
        public static string Wrapp(IEnumerable<Node> nodes)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<table>\n" +
                                 " <thead>\n" +
                                 "     <tr>\n" +
                                 "         <th scope=\"col\">&nbsp;\u2116</th>\n" +
                                 "         <th scope=\"col\"><span style=\"\">Підмножина</span></th>\n" +
                                 "         <th scope=\"col\"><span style=\"\">Оцінка</span></th>\n" +
                                 "         <th scope=\"col\"><span style=\"\">Статус</span></th>\n" +
                                 "     </tr>\n" +
                                 " </thead>\n" +
                                 " <tbody>\n");

            int counter = 1;
            foreach (Node? node in nodes)
            {
                stringBuilder.Append("     <tr>\r\n" +
									 $"         <td>&nbsp;{counter++}</td>\n" +
                                     "         <td>{1:SA:=" + $"{node.Path}" + "}</td>\n" +
                                     "         <td>{1:SA:=" + $"{node.Cost}" + "}</td>\n");

                switch (node.Status)
                {
                    case NodeStatus.Branched:
                        stringBuilder.Append(
                            "         <td>{1:MC:=розгалужена~виключена&nbsp;за ВД~виключена&nbsp;за тестом~рекорд}</td>\n");
                        break;

                    case NodeStatus.ExcludedByVD:
                        stringBuilder.Append(
                            "         <td>{1:MC:розгалужена~=виключена&nbsp;за ВД~виключена&nbsp;за тестом~рекорд}</td>\n");
                        break;

                    case NodeStatus.ExcludedByTest:
                        stringBuilder.Append(
                            "         <td>{1:MC:розгалужена~виключена&nbsp;за ВД~=виключена&nbsp;за тестом~рекорд}</td>\n");
                        break;

                    case NodeStatus.Record:
                        stringBuilder.Append(
                            "         <td>{1:MC:розгалужена~виключена&nbsp;за ВД~виключена&nbsp;за тестом~=рекорд}</td>\n");
                        break;
                }

                stringBuilder.Append("     </tr>\n");
            }

            string endOfHtml = " </tbody>\n" +
                               "</table>";

            stringBuilder.Append(endOfHtml);

            return stringBuilder.ToString();
        }
    }
}