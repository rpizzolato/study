type TweetProps = {
    //texto não sendo obrigatório
    //text?: string;

    //é obrigatório ter um text
    text: string;
}

export function Tweet(props : TweetProps) {
    return (
        <p>{props.text}</p>
    );
}