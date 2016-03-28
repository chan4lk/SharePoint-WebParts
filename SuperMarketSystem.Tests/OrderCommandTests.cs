#region Imports
using NUnit.Framework;
using Moq;
using SuperMarketSystem.Repository;
using SuperMarketSystem.Models;
using SuperMarketSystem.Commands;
using Should;
using SuperMarketSystem.Common;
using SuperMarketSystem.Diagnostics;
using Microsoft.Practices.Unity; 
#endregion

namespace SuperMarketSystem.Tests
{
    /// <summary>
    /// Test for order command.
    /// </summary>
    [TestFixture]
    public class OrderCommandTests
    {
        /// <summary>
        /// The repository.
        /// </summary>
        IRepository<Order> repository;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(r => r.Create(It.IsAny<Order>())).Returns(1);
            mock.Setup(r => r.Create(null)).Returns(-1);
            this.repository = (IRepository<Order>)mock.Object;
            ConfigurationManager.Container.RegisterType<ILogger, MockLogger>();
        }

        /// <summary>
        /// This test should fail.
        /// </summary>
        [Test]
        public void SharepointFailTest()
        {
            CreateOrderCommand command = new CreateOrderCommand(this.repository);
            command.Order.ShouldBeNull();
        }

        /// <summary>
        /// Returns 1 on null.
        /// </summary>
        [Test]
        public void ReturnOnNullTest()
        {
            CreateOrderCommand command = new CreateOrderCommand(this.repository);
            command.Order = null;
            command.Execute();
            command.Order.Id.ShouldBeSameAs(-1);
        }

        /// <summary>
        /// Should pass on any order.
        /// </summary>
        [Test]
        public void ShouldPassOnAnyOrder()
        {
            CreateOrderCommand command = new CreateOrderCommand(this.repository);
            command.Order = new Order();
            command.Execute();
            command.Order.Id.ShouldEqual(1);
        }
    }
}
