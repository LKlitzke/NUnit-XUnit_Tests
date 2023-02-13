using Moq;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SparkyXUnitTest
{
    public class BankAccountXUnitTests
    {
        // Utilizando MOQ
        [Fact]
        public void BankDeposit_Add100_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message("Deposit Invoked"));

            // Arrange
            BankAccount bankAccount = new(logMock.Object);

            // Act
            var result = bankAccount.Deposit(100);

            // Assert
            Assert.True(result);
            Assert.Equal(100,bankAccount.GetBalance());
        }

        [Theory]
        [InlineData(200, 100)]
        [InlineData(200, 199)] // Falha com >= 200
        public void BankWithdrawal_Withdrawal100With200Balance_ReturnsTrue(int balance, int withdrawal)
        {
            var logMock = new Mock<ILogBook>();

            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdrawal);

            Assert.True(result);
        }

        [Fact]
        public void BankWithdrawal_Withdrawal300With200Balance_ReturnsFalse()
        {
            var logMock = new Mock<ILogBook>();

            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            //logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(200);
            var result = bankAccount.Withdraw(300);

            Assert.False(result);
        }

        [Fact]
        public void BankLogDummy_LogMockString_ReturnsTrue()
        {
            var logMock = new Mock<ILogBook>();
            string output = "hello";

            logMock.Setup(u => u.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());

            Assert.Equal(output, logMock.Object.MessageWithReturnStr("Hello"));
        }

        [Fact]
        public void BankLogDummy_LogMockStringOutputStr_ReturnsTrue()
        {
            var logMock = new Mock<ILogBook>();
            string output = "hello";

            logMock.Setup(u => u.LogWithOutputResult(It.IsAny<string>(), out output)).Returns(true);
            string result = "";

            Assert.True(logMock.Object.LogWithOutputResult("Ben", out result));
            Assert.Equal(output,result);
        }

        [Fact]
        public void BankLogDummy_LogRefChecker_ReturnsTrue()
        {
            var logMock = new Mock<ILogBook>();
            Customer customer = new Customer();
            Customer customerNotUsed = new();

            logMock.Setup(u => u.LogWithRefObj(ref customer)).Returns(true);

            Assert.True(logMock.Object.LogWithRefObj(ref customer));
            Assert.False(logMock.Object.LogWithRefObj(ref customerNotUsed));
        }

        [Fact]
        public void BankLogDummy_SetAndGetLogTypeAndSeverityMock_MockTest()
        {
            var logMock = new Mock<ILogBook>();
            logMock.SetupAllProperties();
            logMock.Setup(u => u.LogSeverity).Returns(10);
            logMock.Setup(u => u.LogType).Returns("Warning");

            // Não é possível atribuir valores ao logMock sem o SetupAllPropertis prévio!
            logMock.Object.LogSeverity = 100;

            Assert.Equal(10,logMock.Object.LogSeverity);
            Assert.Equal("Warning",logMock.Object.LogType);

            // Callbacks
            // #1
            string logTemp = "Hello, ";
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                .Returns(true).Callback((string str) => logTemp += str);
            logMock.Object.LogToDb("Ben");

            Assert.Equal("Hello, Ben",logTemp);

            // #2
            // Realizando o callback antes do retorno, logo soma + 2
            int counter = 16;
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                .Callback(() => counter++)
                .Returns(true).Callback(() => counter++);

            logMock.Object.LogToDb("Teste");
            logMock.Object.LogToDb("Teste2");

            Assert.Equal(20,counter);
        }

        [Fact]
        public void BankLogDummy_VerifyExample()
        {
            var logMock = new Mock<ILogBook>();
            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(100);

            Assert.Equal(100,bankAccount.GetBalance());

            // Verificação

            // Quantas vezes é executado
            logMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(2));

            // Quantas vezes é executado com a mensagem especficada
            logMock.Verify(u => u.Message("Test"), Times.AtLeastOnce);

            // Quantas vezes o set/get é executado
            logMock.VerifySet(u => u.LogSeverity = 101, Times.Once);
            logMock.VerifyGet(u => u.LogSeverity, Times.Once);
        }
    }
}
