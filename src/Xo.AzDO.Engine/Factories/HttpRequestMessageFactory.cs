using System.Text;

namespace Xo.AzDO.Engine;

public static class HttpRequestMessageFactory
{
	public static HttpRequestMessage Create(
		Uri url,
		string json,
		HttpMethod httpMethod,
		string mediaType = "application/json"
	)
		=> new HttpRequestMessage(httpMethod, url)
		{
			Content = new StringContent(json, Encoding.UTF8, mediaType)
		};

	public static HttpRequestMessage Create(
		Uri url,
		string mediaType = "application/json"
	)
		=> new HttpRequestMessage(HttpMethod.Get, url);
}
