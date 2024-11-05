using Yoakke.Collections;
using Yoakke.Streams;
using Yoakke.SynKit.Lexer;
using Yoakke.SynKit.Text;

namespace Cesium.Preprocessor;

internal class PreprocessorCharStream<TTokenType> : ICharStream
{
    private IEnumerator<IToken<TTokenType>> underlying;
    /// <inheritdoc/>
    public Position Position { get; private set; }

    /// <inheritdoc/>
    public bool IsEnd => !this.TryPeek(out _);

    /// <inheritdoc/>
    public ISourceFile SourceFile => this.currentToken?.Location.File ?? new SourceFile("<no-location>", "");

    private readonly RingBuffer<(char Char, IToken<TTokenType> Token)> peek = new();
    private char prevChar;
    private IToken<TTokenType>? currentToken;

    /// <summary>
    /// Initializes a new instance of the <see cref="PreprocessorCharStream"/> class.
    /// </summary>
    /// <param name="underlying">The unerlying <see cref="IEnumerable<IToken<T>>"/> to read from.</param>
    public PreprocessorCharStream(IEnumerable<IToken<TTokenType>> underlying)
    {
        this.underlying = underlying.GetEnumerator();
    }

    /// <inheritdoc/>
    public bool TryPeek(out char ch) => this.TryLookAhead(0, out ch);

    /// <inheritdoc/>
    public bool TryLookAhead(int offset, out char ch)
    {
        while (this.peek.Count <= offset)
        {
            var next = this.underlying.MoveNext();
            if (!next)
            {
                ch = default;
                return false;
            }

            foreach (var c in this.underlying.Current.Text)
            {
                this.peek.AddBack((c, this.underlying.Current));
            }
        }
        var currentCharacter = this.peek[offset];
        ch = currentCharacter.Char;
        if (currentToken != currentCharacter.Token)
        {
            currentToken = currentCharacter.Token;
            Position = currentToken.Range.Start;
        }

        return true;
    }

    /// <inheritdoc/>
    public bool TryConsume(out char ch)
    {
        if (!this.TryPeek(out ch)) return false;
        var current = this.peek.RemoveFront();
        this.Position = NextPosition(this.Position, this.prevChar, current);
        this.prevChar = current.Char;
        return true;
    }

    /// <inheritdoc/>
    public int Consume(int amount) => StreamExtensions.Consume(this, amount);

    private static Position NextPosition(Position pos, char lastChar, (char Char, IToken<TTokenType> Token) currentChar)
    {
        // Windows-style, already advanced line at \r
        if (lastChar == '\r' && currentChar.Char == '\n') return pos;
        if (currentChar.Char == '\r' || currentChar.Char == '\n') return pos.Newline();
        if (char.IsControl(currentChar.Char)) return pos;
        return pos.Advance();
    }

    /// <inheritdoc/>
    public void Defer(char item) => throw new NotSupportedException();
}
