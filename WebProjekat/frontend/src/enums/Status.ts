export const Status = {
    PLANNED : "PLANNED",
    FINISHED : "FINISHED",
    CANCELLED : "CANCELLED",
    RESERVED : "RESERVED"
} as const;

export type Status = typeof Status[keyof typeof Status];