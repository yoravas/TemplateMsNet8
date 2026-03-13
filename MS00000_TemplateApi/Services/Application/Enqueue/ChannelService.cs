using System.Threading.Channels;

namespace MS00000_TemplateApi.Services.Application.Enqueue;

public class ChannelService<T> : IChannelService<T>
{
    private readonly Channel<T> channel;

    public ChannelService(IConfiguration configuration)
    {
        int codaMax = configuration.GetValue<int>("CodaMax");
        channel = Channel.CreateBounded<T>(new BoundedChannelOptions(codaMax)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true,
            SingleWriter = false
        });
    }

    public ChannelReader<T> Reader => channel.Reader;

    public ChannelWriter<T> Writer => channel.Writer;

    public void CompleteWriter(Exception? error = null)
    {
        if (error is null)
            channel.Writer.TryComplete();
        else
            channel.Writer.TryComplete(error);
    }

    public ValueTask<T> ReadAsync(CancellationToken ct = default) => channel.Reader.ReadAsync(ct);

    public ValueTask WriteAsync(T item, CancellationToken ct = default) => channel.Writer.WriteAsync(item, ct);
}