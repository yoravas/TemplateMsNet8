using System.Threading.Channels;

namespace MS00000_TemplateApi.Services.Application.Enqueue;

public interface IChannelService<T>
{
    ChannelReader<T> Reader { get; }
    ChannelWriter<T> Writer { get; }

    ValueTask WriteAsync(T item, CancellationToken ct = default);

    ValueTask<T> ReadAsync(CancellationToken ct = default);

    void CompleteWriter(Exception? error = null);
}