import React from "react";
import { cn } from "../lib/utils";

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: "default" | "outline" | "ghost";
}

export const Button: React.FC<ButtonProps> = ({
  className,
  variant = "default",
  ...props
}) => {
  const base =
    "px-4 py-2 font-medium rounded-md transition-all active:scale-[0.97] disabled:opacity-50 disabled:cursor-not-allowed";

  const variants = {
    default: "bg-blue-600 text-white hover:bg-blue-700",
    outline:
      "border border-gray-300 text-gray-700 hover:bg-gray-100",
    ghost:
      "text-gray-700 hover:bg-gray-100",
  };

  return (
    <button
      className={cn(base, variants[variant], className)}
      {...props}
    />
  );
};
