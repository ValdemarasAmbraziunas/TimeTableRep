export function parseBoolean(value: string): boolean {
    if (value) {
        if (value.toLowerCase().trim() === "true") return true;
    } 
    return false;
}