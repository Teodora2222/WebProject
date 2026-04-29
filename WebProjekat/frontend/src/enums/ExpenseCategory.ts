export const ExpenseCategory ={
  TRANSPORT : "Transport",
  ACCOMMODATION : "Accommodation",
  FOOD : "Food",
  TICKETS : "Tickets",
  SHOPPING : "Shopping",
  OTHER : "Other"
} as const;

export type ExpenseCategory = typeof ExpenseCategory[keyof typeof ExpenseCategory];