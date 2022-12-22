export async function CreateObjectUrlFromStream(imageStream) {
    const arrayBuffer = await imageStream.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    return url;
}

export function disposeObject(url) {
    URL.revokeObjectURL(url);
}
