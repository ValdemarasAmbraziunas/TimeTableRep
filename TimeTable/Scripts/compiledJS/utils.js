"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function parseBoolean(value) {
    if (value) {
        if (value.toLowerCase().trim() === "true")
            return true;
    }
    return false;
}
exports.parseBoolean = parseBoolean;
