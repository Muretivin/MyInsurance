using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyInsurance.Data;
using MyInsurance.Models;
using MyInsurance.Models.Domain;
using System.Linq;

namespace MyInsurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly InsuranceDbContext dbContext;

        public InsuranceController(InsuranceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllInsurances(string? clientName = null, string? policyType = null)
        {
            var query = dbContext.Insurances.AsQueryable();

            // Apply filtering
            if (!string.IsNullOrWhiteSpace(clientName))
            {
                query = query.Where(i => i.ClientName.Contains(clientName));
            }

            if (!string.IsNullOrWhiteSpace(policyType))
            {
                query = query.Where(i => i.PolicyType == policyType);
            }

            var insurances = query.ToList();
            return Ok(insurances);
        }

        [HttpPost]
        public IActionResult AddInsurance(AddInsuranceRequestDTO request)
        {
            var domainModelInsurance = new Insurance
            {
                Id = Guid.NewGuid(),
                PolicyNumber = request.PolicyNumber,
                ClientName = request.ClientName,
                PolicyType = request.PolicyType,
                PremiumAmount = request.PremiumAmount,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            dbContext.Insurances.Add(domainModelInsurance);
            dbContext.SaveChanges();
            return Ok(domainModelInsurance);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateInsurance(Guid id, UpdateInsuranceRequestDTO request)
        {
            var insurance = dbContext.Insurances.Find(id);

            if (insurance is null)
            {
                return NotFound("Insurance policy not found.");
            }

            // Update the insurance details
            insurance.PolicyNumber = request.PolicyNumber;
            insurance.ClientName = request.ClientName;
            insurance.PolicyType = request.PolicyType;
            insurance.PremiumAmount = request.PremiumAmount;
            insurance.StartDate = request.StartDate;
            insurance.EndDate = request.EndDate;

            dbContext.SaveChanges();
            return Ok(insurance);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteInsurance(Guid id)
        {
            var insurance = dbContext.Insurances.Find(id);

            if (insurance is not null)
            {
                dbContext.Insurances.Remove(insurance);
                dbContext.SaveChanges();
            }

            return Ok();
        }
    }
}
