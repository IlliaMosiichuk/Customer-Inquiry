using API.Models;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]InquiryViewModel inquiry)
        {
            var validationResult = inquiry.GetValidationResult();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            Customer customer = null;
            if (!string.IsNullOrEmpty(inquiry.Email))
            {
                customer = _customerRepository.GetByEmail(inquiry.Email);
            }
            else if (inquiry.CustomerId.HasValue)
            {
                customer = _customerRepository.GetById(inquiry.CustomerId.Value);
            }
            else
            {
                customer = _customerRepository.GetByEmailAndId(inquiry.Email, inquiry.CustomerId.Value);
            }

            if (customer == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<CustomerViewModel>(customer);
            return Ok(model);
        }
    }
}
