def prettyfy(text):
    return ' '.join([text[i:i + 4] for i in range(0, len(text), 4)])


def unprettyfy(text):
    return text.replace(" ", "")


def grid_prettyfy(text):
    text = prettyfy(text)

    text = text.split(" ")
    for i, word in enumerate(text):
        if i != 0 and i % 5 == 0:
            text[i] = word + '\n'

    text = ' '.join(text)
    return text