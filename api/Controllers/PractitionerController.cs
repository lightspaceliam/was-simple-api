using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]s")]
public class PractitionerController : ControllerBase
{
	private readonly ILogger<PractitionerController> _logger;
	private readonly IConfiguration _configuration;

	public PractitionerController(ILogger<PractitionerController> logger, IConfiguration configuration)
	{
		_logger = logger;
		_configuration = configuration;
	}

	[HttpGet]
	public IActionResult Index()
	{
		var version = _configuration.GetValue<string>("Version") ?? "N/A";
		var when = new DateTimeOffset(DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc));
		var bundle = new Bundle
		{
			Type = Bundle.BundleType.Searchset,
			Meta = new Meta
			{
				LastUpdated = when,
				Extension = new List<Extension>
				{
					new Extension(url: "http://aws-simple-api.com/version", value: new FhirString(version))
				}
			},
			Entry = new List<Bundle.EntryComponent>
			{
				new Bundle.EntryComponent
				{
					Resource = new Practitioner
					{
						Id = "1",
						Active = true,
						Name = new List<HumanName>
						{
							new HumanName
							{
								Prefix = new string[] { "Dr." },
								Family = "Howser",
								Given = new string[] { "Doogie" },
								Suffix = new string[] { "MD" }
							}
						},
						Telecom = new List<ContactPoint>
						{
							new ContactPoint(
								system: ContactPoint.ContactPointSystem.Email,
								use: ContactPoint.ContactPointUse.Work,
								value: "doogie.howser@aws-simple-api.com")
						}
					}
				},
				new Bundle.EntryComponent
				{
					Resource = new Practitioner
					{
						Id = "2",
						Active = true,
						Name = new List<HumanName>
						{
							new HumanName
							{
								Prefix = new string[] { "Dr." },
								Family = "Canfield",
								Given = new string[] { "Benjamin" },
							}
						},
						Telecom = new List<ContactPoint>
						{
							new ContactPoint(
								system: ContactPoint.ContactPointSystem.Email,
								use: ContactPoint.ContactPointUse.Work,
								value: "benjamin.canfield@aws-simple-api.com")
						}
					}
				}
			}
		};
		
		return Ok(bundle);
	}
}