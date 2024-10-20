using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace api.Configuration;

public class FhirResourceConverter : JsonConverter<Resource>
{
	public override Resource ReadJson(JsonReader reader, Type objectType, Resource? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		var value = JRaw.Create(reader).ToString();
		return FhirJsonNode.Parse(value).ToPoco<Resource>();
	}

	public override void WriteJson(JsonWriter writer, Resource? value, JsonSerializer serializer)
	{
		var fhirJsonSerializer = new FhirJsonSerializer();
		writer.WriteRaw(fhirJsonSerializer.SerializeToString(value));
	}
}