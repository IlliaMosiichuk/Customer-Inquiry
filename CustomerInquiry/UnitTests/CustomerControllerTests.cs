using API.Controllers;
using API.Models;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        [TestCase(null, null)]
        [TestCase("invalidemail", null)]
        [TestCase(null, -1)]
        [TestCase("invalidemail", -1)]
        public void Get_InvalidParameters_BadRequest(string email, long? customerId)
        {
            var inquiry = new InquiryViewModel()
            {
                Email = email,
                CustomerId = customerId,
            };
            var mapperMock = new Mock<IMapper>();
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var controller = new CustomersController(customerRepositoryMock.Object, mapperMock.Object);

            var result = controller.Get(inquiry);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [TestCase("notexistingemail@domain.com", null)]
        [TestCase(null, 123)]
        [TestCase("notexistingemail@domain.com", 123)]
        public void Get_NotExistingParameters_NotFound(string email, long? customerId)
        {
            var inquiry = new InquiryViewModel()
            {
                Email = email,
                CustomerId = customerId,
            };
            var mapperMock = new Mock<IMapper>();
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var controller = new CustomersController(customerRepositoryMock.Object, mapperMock.Object);

            var result = controller.Get(inquiry);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        public void Get_ExistingEmail_Ok()
        {
            var testData = GetTestCustomerData();
            var inquiry = new InquiryViewModel()
            {
                Email = testData.Customer.Email,
            };
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CustomerViewModel>(testData.Customer))
                .Returns(testData.CustomerViewModel).Verifiable();

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.GetByEmail(inquiry.Email))
                .Returns(testData.Customer).Verifiable();

            var controller = new CustomersController(customerRepositoryMock.Object, mapperMock.Object);

            var result = controller.Get(inquiry);

            var model = (result as OkObjectResult).Value as CustomerViewModel;
            mapperMock.Verify();
            customerRepositoryMock.Verify();
            Assert.AreEqual(testData.CustomerViewModel.Email, model.Email);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        public void Get_ExistingCustomerId_Ok()
        {
            var testData = GetTestCustomerData();
            var inquiry = new InquiryViewModel()
            {
                CustomerId = testData.Customer.Id,
            };
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CustomerViewModel>(testData.Customer))
                .Returns(testData.CustomerViewModel).Verifiable();

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.GetById(inquiry.CustomerId.Value))
                .Returns(testData.Customer).Verifiable();

            var controller = new CustomersController(customerRepositoryMock.Object, mapperMock.Object);

            var result = controller.Get(inquiry);

            var model = (result as OkObjectResult).Value as CustomerViewModel;
            mapperMock.Verify();
            customerRepositoryMock.Verify();
            Assert.AreEqual(testData.CustomerViewModel.Id, model.Id);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        public void Get_ExistingEmailAndCustomerId_Ok()
        {
            var testData = GetTestCustomerData();
            var inquiry = new InquiryViewModel()
            {
                Email = testData.Customer.Email,
                CustomerId = testData.Customer.Id,
            };
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CustomerViewModel>(testData.Customer))
                .Returns(testData.CustomerViewModel).Verifiable();

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.GetByEmailAndId(inquiry.Email, inquiry.CustomerId.Value))
                .Returns(testData.Customer).Verifiable();

            var controller = new CustomersController(customerRepositoryMock.Object, mapperMock.Object);

            var result = controller.Get(inquiry);

            var model = (result as OkObjectResult).Value as CustomerViewModel;
            mapperMock.Verify();
            customerRepositoryMock.Verify();
            Assert.AreEqual(testData.CustomerViewModel.Id, model.Id);
            Assert.AreEqual(testData.CustomerViewModel.Email, model.Email);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        public (Customer Customer, CustomerViewModel CustomerViewModel) GetTestCustomerData()
        {
            var customer = new Customer()
            {
                Id = 1,
                Email = "user@domain.com",
                Mobile = "0123456789",
                Name = "Firstname Lastname",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Id = 1,
                        Date = new DateTime(2018, 3, 31, 21, 23, 0),
                        Amount = 1234.56m,
                        Currency = "USD",
                        Status = ETransactionStatus.Success
                    }
                }
            };

            var customerViewModel = new CustomerViewModel()
            {
                Id = customer.Id,
                Email = customer.Email,
                Mobile = customer.Mobile,
                Name = customer.Name,
                Transactions = customer.Transactions.Select(t => new TransactionViewModel()
                {
                    Id = t.Id,
                    Date = t.Date.ToString(),
                    Amount = t.Amount.ToString(),
                    Currency = t.Currency,
                    Status = t.Status.ToString(),
                }).ToList()
            };

            return (customer, customerViewModel);
        }


    }
}
