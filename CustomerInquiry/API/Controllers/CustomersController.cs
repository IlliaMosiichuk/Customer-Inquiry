using API.Models;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiVersion("1.0")]
    public class CustomersController : BaseApiController
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper,
            ILogger<CustomersController> logger)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("GetDetails")]
        public IActionResult GetDetails([FromQuery]InquiryViewModel inquiry)
        {
            _logger.LogInformation($"GetDetails() method was called with parameters: email: {inquiry.Email}, customerId: {inquiry.CustomerId}");

            var validationResult = inquiry.GetValidationResult();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            Customer customer = null;
            if (!string.IsNullOrEmpty(inquiry.Email) && inquiry.CustomerId.HasValue)
            {
                customer = _customerRepository.GetByEmailAndId(inquiry.Email, inquiry.CustomerId.Value);
            }
            else if (inquiry.CustomerId.HasValue)
            {
                customer = _customerRepository.GetById(inquiry.CustomerId.Value);
            }
            else
            {
                customer = _customerRepository.GetByEmail(inquiry.Email);
            }

            if (customer == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<CustomerViewModel>(customer);
            return Ok(model);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var customers = _customerRepository.GetAll();

            var model = _mapper.Map<List<CustomerViewModel>>(customers);

            return Ok(model);
        }
    }
}
