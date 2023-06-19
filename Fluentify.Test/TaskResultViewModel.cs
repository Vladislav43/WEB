using Fluentify.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fluentify.Tests
{
    [TestClass]
    public class TaskResultViewModelTests
    {
        [TestMethod]
        public void TaskResultViewModel_Initialization()
        {
            // Arrange
            var taskResultViewModel = new TaskResultViewModel();

            // Assert
            Assert.IsNotNull(taskResultViewModel);
        }

        [TestMethod]
        public void TaskResultViewModel_SetProperties()
        {
            // Arrange
            var taskResultViewModel = new TaskResultViewModel();

            // Act
            taskResultViewModel.Question = "What is the result of 2 + 2?";
            taskResultViewModel.UserAnswer = "4";
            taskResultViewModel.CorrectAnswer = "4";
            taskResultViewModel.IsCorrect = true;
            taskResultViewModel.Score = 1;

            // Assert
            Assert.AreEqual("What is the result of 2 + 2?", taskResultViewModel.Question);
            Assert.AreEqual("4", taskResultViewModel.UserAnswer);
            Assert.AreEqual("4", taskResultViewModel.CorrectAnswer);
            Assert.IsTrue(taskResultViewModel.IsCorrect);
            Assert.AreEqual(1, taskResultViewModel.Score);
        }
    }
}