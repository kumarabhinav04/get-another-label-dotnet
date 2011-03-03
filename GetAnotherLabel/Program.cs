using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GetAnotherLabel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string inputfile = "";
            string correctfile = "";
            string costfile = "";
            int iterations = 10;

            if (args.Length != 4) {
                Console.WriteLine("Usage: GetAnotherLabel <inputfile> <correctfile> <costfile> <iterations>");
                Console.WriteLine("");
                Console.WriteLine("Example: GetAnotherLabel data\\unlabeled.txt data\\labeled.txt  data\\costs.txt 10");
                Console.WriteLine("");
                Console.WriteLine("The <inputfile> is a tab-separated text file.");
                Console.WriteLine("Each line has the form <workerid><tab><objectid><tab><assigned_label>");
                Console.WriteLine("and records the label that the given worker gave to that object");
                Console.WriteLine("");
                Console.WriteLine("The <correctfile> is a tab-separated text file.");
                Console.WriteLine("Each line has the form <workerid><tab><objectid><tab><assigned_label>");
                Console.WriteLine("and records the correct labels for whatever objects we have them.");
                Console.WriteLine("The workerid is ignored for the <correctfile>");
                Console.WriteLine("");
                Console.WriteLine("The <costfile> is a tab-separated text file.");
                Console.WriteLine("Each line has the form <from_class><tab><to_class><tab><classification_cost>");
                Console.WriteLine("and records the classification cost of classifying an object that belongs to the `from_class` into the `to_class`.");
                Console.WriteLine("");
                Console.WriteLine("<iterations> is the number of times to run the algorithm. Even a value of 1 works well.");
                Environment.Exit(-1);
            } else {
                inputfile = args[0];
                correctfile = args[1];
                costfile = args[2];
                iterations = int.Parse(args[3]);
            }

            string[] lines_input = File.ReadAllLines(inputfile);
            List<Labeling> labelings = DawidSkene.LoadLabels(lines_input);
            string[] lines_correct = File.ReadAllLines(correctfile);
            List<Labeling> correct = DawidSkene.LoadLabels(lines_correct);
            string[] lines_cost = File.ReadAllLines(costfile);
            List<Labeling> costs = DawidSkene.LoadLabels(lines_cost);

            DawidSkene ds = new DawidSkene(labelings, correct, costs);
            
            // Save the majority vote before the D&S estimation
            Dictionary<string, string> prior_voting = ds.GetMajorityVote();
            File.WriteAllText("pre-majority-vote.txt", ds.PrintVote());

            ds.Estimate(iterations);
            ds.UpdateAnnotatorCosts();

            // Save the estimated cost for each worker
            File.WriteAllText("worker-costs.txt", ds.PrintAnnotatorCostsSummary());

            // Save the error rates for the workers
            File.WriteAllText("worker-error-rates.txt", ds.PrintAllWorkerScores());

            // Save the probability that an object belongs to each class
            File.WriteAllText("object-probabilities.txt", ds.PrintObjectClassProbabilities(0.0));

            // Save the majority vote after the D&S estimation
            Dictionary<string, string> posterior_voting = ds.GetMajorityVote();
            File.WriteAllText("post-majority-vote.txt", ds.PrintVote());

            // Save the probability that an object belongs to each class
            File.WriteAllText("differences-pre-post-majority-vote.txt", ds.PrintDiffVote(prior_voting, posterior_voting));
        }
    }
}
