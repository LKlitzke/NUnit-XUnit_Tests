using Moq;
using NUnit.Framework;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyNUnitTest
{
    [TestFixture]
    public class BankAccountNUnitTests
    {
        private BankAccount account;

        [SetUp]
        public void Setup()
        {
            
        }

        /*[Test]
        public void BankDepositLogFaker_Add100_ReturnTrue()
        {
            // Arrange
            BankAccount bankAccount = new(new LogFaker());

            // Act
            var result = bankAccount.Deposit(100);

            // Assert
            Assert.IsTrue(result);
            Assert.That(bankAccount.GetBalance,Is.EqualTo(100));
        }*/

        // Utilizando MOQ
        [Test]
        public void BankDeposit_Add100_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message("Deposit Invoked"));

            // Arrange
            BankAccount bankAccount = new(logMock.Object);

            // Act
            var result = bankAccount.Deposit(100);

            // Assert
            Assert.IsTrue(result);
            Assert.That(bankAccount.GetBalance, Is.EqualTo(100));
        }

        [Test]
        [TestCase(200,100)]
        [TestCase(200,199)] // Falha com >= 200
        public void BankWithdrawal_Withdrawal100With200Balance_ReturnsTrue(int balance, int withdrawal)
        {
            var logMock = new Mock<ILogBook>();

            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdrawal);

            Assert.IsTrue(result);
        }

        [Test]
        public void BankWithdrawal_Withdrawal300With200Balance_ReturnsFalse()
        {
            var logMock = new Mock<ILogBook>();

            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            //logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.IsInRange<int>(int.MinValue,-1,Moq.Range.Inclusive))).Returns(false);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(200);
            var result = bankAccount.Withdraw(300);

            Assert.IsFalse(result);
        }

        [Test]
        public void BankLogDummy_LogMockString_ReturnsTrue()
        {
            var logMock = new Mock<ILogBook>();
            string output = "hello";

            logMock.Setup(u => u.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());

            Assert.That(logMock.Object.MessageWithReturnStr("Hello"),Is.EqualTo(output));
        }

        [Test]
        public void BankLogDummy_LogMockStringOutputStr_ReturnsTrue()
        {
            var logMock = new Mock<ILogBook>();
            string output = "hello";

            logMock.Setup(u => u.LogWithOutputResult(It.IsAny<string>(),out output)).Returns(true);
            string result = "";

            Assert.IsTrue(logMock.Object.LogWithOutputResult("Ben", out result));
            Assert.That(result, Is.EqualTo(output));
        }

        [Test]
        public void BankLogDummy_LogRefChecker_ReturnsTrue()
        {
            var logMock = new Mock<ILogBook>();
            Customer customer = new Customer();
            Customer customerNotUsed = new();

            logMock.Setup(u => u.LogWithRefObj(ref customer)).Returns(true);

            Assert.IsTrue(logMock.Object.LogWithRefObj(ref customer));
            Assert.IsFalse(logMock.Object.LogWithRefObj(ref customerNotUsed));
        }

        [Test]
        public void BankLogDummy_SetAndGetLogTypeAndSeverityMock_MockTest()
        {
            var logMock = new Mock<ILogBook>();
            logMock.SetupAllProperties();
            logMock.Setup(u => u.LogSeverity).Returns(10);
            logMock.Setup(u => u.LogType).Returns("Warning");
            
            // Não é possível atribuir valores ao logMock sem o SetupAllPropertis prévio!
            logMock.Object.LogSeverity = 100;

            Assert.That(logMock.Object.LogSeverity, Is.EqualTo(10));
            Assert.That(logMock.Object.LogType, Is.EqualTo("Warning"));

            // Callbacks
            // #1
            string logTemp = "Hello, ";
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                .Returns(true).Callback((string str) => logTemp += str);
            logMock.Object.LogToDb("Ben");

            Assert.That(logTemp, Is.EqualTo("Hello, Ben"));

            // #2
            // Realizando o callback antes do retorno, logo soma + 2
            int counter = 16;
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                .Callback(() => counter++)
                .Returns(true).Callback(() => counter++);

            logMock.Object.LogToDb("Teste");
            logMock.Object.LogToDb("Teste2");

            Assert.That(counter, Is.EqualTo(20));
        }

        [Test]
        public void BankLogDummy_VerifyExample()
        {
            var logMock = new Mock<ILogBook>();
            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(100);

            Assert.That(bankAccount.GetBalance, Is.EqualTo(100));

            // Verificação

            // Quantas vezes é executado
            logMock.Verify(u => u.Message(It.IsAny<string>()),Times.Exactly(2));

            // Quantas vezes é executado com a mensagem especficada
            logMock.Verify(u => u.Message("Test"), Times.AtLeastOnce);

            // Quantas vezes o set/get é executado
            logMock.VerifySet(u => u.LogSeverity = 101, Times.Once);
            logMock.VerifyGet(u => u.LogSeverity, Times.Once);
        }
    }
}
