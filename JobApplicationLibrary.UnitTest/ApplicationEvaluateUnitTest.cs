using JobApplicationLibrary.Models;
using JobApplicationLibrary.Services;
using Moq;
using NUnit.Framework;
using static JobApplicationLibrary.ApplicationEvaluator;

namespace JobApplicationLibrary.UnitTest
{
    public class ApplicationEvaluateUnitTest
    {

        [Test]
        public void Application_WithUnderAge_TransferredToAutoRejected()
        {
            var evaluator = new ApplicationEvaluator(null);
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
            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);
            var evaluator = new ApplicationEvaluator(mockValidator.Object);  

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
            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age=18},
                TechStackList = new System.Collections.Generic.List<string>() {"C#","RabbitMQ","MicroService","Visual Studio" },
                YearsOfExperience=16
            };

            var appResult = evaluator.Evaluate(form);
            Assert.AreEqual(appResult, ApplicationResult.AutoAccepted);
        }

        [Test]
        public void Application_WithInValidIdentityNumber_TransferredToHR()
        {
            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() {
                    Age = 18
                },
               
            };
            var appResult = evaluator.Evaluate(form);
            Assert.AreEqual(appResult, ApplicationResult.TransferredToHR);
        }
    }
}