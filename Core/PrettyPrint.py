def prettyfy(text):
    return ' '.join([text[i:i + 4] for i in range(0, len(text), 4)])


def unprettyfy(text):
    return text.replace(" ", "")


def grid_prettyfy(text, group_n):
    text = prettyfy(text)

    words = text.split()
    grouped_words = []
    for i in range(0, len(words), group_n):
        grouped_words.append(' '.join(words[i: i + group_n]))

    text = ""
    for group in grouped_words:
        text += group + "\n"
    return text
