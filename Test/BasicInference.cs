using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GetAnotherLabel;

namespace GetAnotherLabel.Test
{
    [TestClass]
    public class BasicInference
    {
        [TestMethod]
        public void TestTiny()
        {
            int iterations = 10;
            string[] lines_input = BasicInference.ReadFile(this.GetType().Assembly.GetManifestResourceStream("GetAnotherLabel.Test.data.data.tiny"));
            List<Labeling> labelings = DawidSkene.LoadLabels(lines_input);
            DawidSkene ds = new DawidSkene(labelings, null, null);

            // Save the majority vote before the D&S estimation
            //HashMap<String,String> prior_voting = ds.getMajorityVote();
            //Utils.writeFile( ds.printVote(), "./pre-majority-vote.txt");

            ds.Estimate(iterations);
            ds.UpdateAnnotatorCosts();

            // Assert the majority vote after the D&S estimation
            Dictionary<string, string> posterior_voting = ds.GetMajorityVote();
            //String v_str=ds.printVote();
            Assert.AreEqual("1", posterior_voting["O1"]);
            Assert.AreEqual("2", posterior_voting["O2"]);
            Assert.AreEqual("3", posterior_voting["O3"]);
            // Save the probability that an object belongs to each class
            //Utils.writeFile(ds.printDiffVote(prior_voting, posterior_voting), "./differences-pre-post-majority-vote.txt");
        }
        [TestMethod]
        public void TestTiny2()
        {
            int iterations = 10;
            string[] lines_input = BasicInference.ReadFile(this.GetType().Assembly.GetManifestResourceStream("GetAnotherLabel.Test.data.data.tiny2"));
            List<Labeling> labelings = DawidSkene.LoadLabels(lines_input);
            DawidSkene ds = new DawidSkene(labelings, null, null);

            // Save the majority vote before the D&S estimation
            //HashMap<String,String> prior_voting = ds.getMajorityVote();
            //Utils.writeFile( ds.printVote(), "./pre-majority-vote.txt");

            ds.Estimate(iterations);
            ds.UpdateAnnotatorCosts();

            // Assert the majority vote after the D&S estimation
            Dictionary<string, string> posterior_voting = ds.GetMajorityVote();
            Assert.AreEqual("1", posterior_voting["O1"]);
            Assert.AreEqual("2", posterior_voting["O2"]);
            Assert.AreEqual("3", posterior_voting["O3"]);
        }
        [TestMethod]
        public void TestTiny3()
        {
            int iterations = 10;
            string[] lines_input = BasicInference.ReadFile(this.GetType().Assembly.GetManifestResourceStream("GetAnotherLabel.Test.data.data.tiny3"));
            List<Labeling> labelings = DawidSkene.LoadLabels(lines_input);
            DawidSkene ds = new DawidSkene(labelings, null, null);

            // Save the majority vote before the D&S estimation
            //HashMap<String,String> prior_voting = ds.getMajorityVote();
            //Utils.writeFile( ds.printVote(), "./pre-majority-vote.txt");

            ds.Estimate(iterations);
            ds.UpdateAnnotatorCosts();

            // Assert the majority vote after the D&S estimation
            Dictionary<string,string> posterior_voting = ds.GetMajorityVote();
            Assert.AreEqual("1", posterior_voting["O1"]);
            Assert.AreEqual("2", posterior_voting["O2"]);
            Assert.AreEqual("3", posterior_voting["O3"]);
        }
        [TestMethod]
        public void TestTiny4()
        {
            int iterations = 10;
            string[] lines_input = BasicInference.ReadFile(this.GetType().Assembly.GetManifestResourceStream("GetAnotherLabel.Test.data.data.tiny4"));
            List<Labeling> labelings = DawidSkene.LoadLabels(lines_input);
            DawidSkene ds = new DawidSkene(labelings, null, null);

            // Save the majority vote before the D&S estimation
            //HashMap<String,String> prior_voting = ds.getMajorityVote();
            //Utils.writeFile( ds.printVote(), "./pre-majority-vote.txt");

            ds.Estimate(iterations);
            ds.UpdateAnnotatorCosts();

            // Assert the majority vote after the D&S estimation
            Dictionary<string, string> posterior_voting = ds.GetMajorityVote();
            Assert.AreEqual("1", posterior_voting["O1"]);
            Assert.AreEqual("2", posterior_voting["O2"]);
        }
        private static string[] ReadFile(Stream s)
        {
            StreamReader sr = new StreamReader(s);
            List<string> lines = new List<string>();
            string line = null;
            while ((line = sr.ReadLine()) != null) {
                lines.Add(line);
            }
            sr.Close();
            return lines.ToArray();
        }
    }
}
