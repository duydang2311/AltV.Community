const interfaceTemplate = `namespace AltV.Community.Events;

public interface IEvent{generic}
{
    bool AddHandler(Action{generic} handler);
    bool AddHandler(Func<{func_generic}Task> handler);
    bool RemoveHandler(Action{generic} handler);
    bool RemoveHandler(Func<{func_generic}Task> handler);
    void Invoke({parameters});
    Task InvokeAsync({parameters});
    Task InvokeAsyncSerial({parameters});
}
`;

const classTemplate = `namespace AltV.Community.Events;

public class Event{generic}(IEventInvoker invoker) : IEvent{generic}
{
    private readonly HashSet<Delegate> delegates = [];

    public bool AddHandler(Action{generic} handler)
    {
        return delegates.Add(handler);
    }

    public bool AddHandler(Func<{func_generic}Task> handler)
    {
        return delegates.Add(handler);
    }

    public bool RemoveHandler(Action{generic} handler)
    {
        return delegates.Remove(handler);
    }

    public bool RemoveHandler(Func<{func_generic}Task> handler)
    {
        return delegates.Remove(handler);
    }

    public void Invoke({parameters})
    {
        invoker.Invoke(delegates{args});
    }

    public Task InvokeAsync({parameters})
    {
        return invoker.InvokeAsync(delegates{args});
    }

    public Task InvokeAsyncSerial({parameters})
    {
        return invoker.InvokeAsyncSerial(delegates{args});
    }
}
`;

const createNumberFilledArray = (length) =>
  new Array(length).fill(0).map((_, i) => i);

for (let k = 0; k !== 8; ++k) {
  const numbers = createNumberFilledArray(k);
  const generic =
    k === 0 ? "" : `<${numbers.map((a) => `T${a + 1}`).join(", ")}>`;
  const funcGeneric =
    k === 0 ? "" : `${numbers.map((a) => `T${a + 1}`).join(", ")}, `;
  const parameters =
    k === 0 ? "" : `${numbers.map((a) => `T${a + 1} arg${a + 1}`).join(", ")}`;
  const args =
    k === 0 ? "" : `, ${numbers.map((a) => `arg${a + 1}`).join(", ")}`;

  const interfaceFilename = `[Generated]/IEvent${k === 0 ? "" : `\`${k}`}.g.cs`;
  const classFilename = `[Generated]/Event${k === 0 ? "" : `\`${k}`}.g.cs`;

  await Bun.write(
    interfaceFilename,
    interfaceTemplate
      .replaceAll("{generic}", generic)
      .replaceAll("{func_generic}", funcGeneric)
      .replaceAll("{parameters}", parameters)
  );

  await Bun.write(
    classFilename,
    classTemplate
      .replaceAll("{generic}", generic)
      .replaceAll("{func_generic}", funcGeneric)
      .replaceAll("{parameters}", parameters)
      .replaceAll("{args}", args)
  );
}
