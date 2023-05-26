﻿namespace Xo.AzDO.Cli.Processors;

internal class WiProcessor : BaseHttpProcessor, IProcessor<CreateWiCmd, WiRes>
{
	private readonly ITypeMapper<CreateWiCmd, ExtWiReq> _extReqMapper;
	const string API_VERSION = "7.0";

	public WiProcessor(
		IHttpClientFactory httpClientFactory,
		ITypeSerializer typeSerializer,
		ITypeMapper<CreateWiCmd, ExtWiReq> typeMapper
	) : base(httpClientFactory, API_VERSION, typeSerializer) => this._extReqMapper = typeMapper ?? throw new ArgumentNullException(nameof(typeMapper));

	public async Task<WiRes> ProcessAsync(CreateWiCmd cmd)
	{
		await this.CoreProcessAsync(cmd);
		return new WiRes { Cmd = cmd };
	}

	private async Task CoreProcessAsync(CreateWiCmd cmd)
	{
		var url = new Uri($"{BASE_URL}/{PROJECT_NAME}/_apis/wit/workitems/${cmd.type}?api-version={this._ApiVersion}");
		var reqContent = this._TypeSerializer.Serialize(this._extReqMapper.Map(cmd).workitem_payload);
		Console.WriteLine(reqContent);
		var httpReq = HttpRequestMessageFactory.Create(url, reqContent, HttpMethod.Post, mediaType: "application/json-patch+json");

		using var httpClient = this.CreateHttpClient();
		var resp = await httpClient.SendAsync(httpReq);
		resp.EnsureSuccessStatusCode();
		var respContent = await resp.Content.ReadAsStringAsync();

		var type = this._TypeSerializer.Deserialize<ExtWiResp>(respContent);
		cmd.ExtResp = type;

		if (cmd.children.Any())
		{
			this.Enrich(cmd);
			await Task.WhenAll(cmd.children.Select(c => this.CoreProcessAsync(c)));
		}
	}

	private void Enrich(CreateWiCmd cmd) => cmd.children.ToList().ForEach(c => c.relation = new Models.WorkItemRelation { relation_type = "Child", url = cmd.ExtResp.url });
}
