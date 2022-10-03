using JobApplicationLibrary.Models;
using NUnit.Framework;
using static JobApplicationLibrary.ApplicationEvaluator;

namespace JobApplicationLibrary.UnitTest
{
    public class ApplicationEvaluateUnitTest
    {

        [Test]
        public void Application_WithUnderAge_TransferredToAutoRejected()
        {
            var evaluator = new ApplicationEvaluator();
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 17
                }
            };

            var appResult =evaluator.Evaluate(form);
            Assert.AreEqual(appResult, ApplicationResult.AutoRejected);
        }

        [Test]
        public void Application_WithNoTechStack_TransferredToAutoRejected()
        {
            var evaluator = new ApplicationEvaluator();
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age=18},
                TechStackList=new System.Collections.Generic.List<string>() {" "}
            };

            var appResult = evaluator.Evaluate(form);
            Assert.AreEqual(appResult, ApplicationResult.AutoRejected);
        }

        [Test]
        public void Application_WithTechOver75P_TransferredToAutoAccepted()
        {
            var evaluator = new ApplicationEvaluator();
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age=18},
                TechStackList = new System.Collections.Generic.List<string>() {"C#","RabbitMQ","MicroService","Visual Studio" },
                YearsOfExperience=16
            };

            var appResult = evaluator.Evaluate(form);
            Assert.AreEqual(appResult, ApplicationResult.AutoAccepted);
        }
    }
}