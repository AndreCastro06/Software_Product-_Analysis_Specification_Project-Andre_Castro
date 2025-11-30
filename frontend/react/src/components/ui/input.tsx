import React from "react";
import { cn } from "../lib/utils";

export const Input = React.forwardRef<HTMLInputElement, React.InputHTMLAttributes<HTMLInputElement>>(
  ({ className, ...props }, ref) => {
    return (
      <input
        ref={ref}
        className={cn(
          "w-full px-3 py-2 border rounded-md bg-white text-gray-900 outline-none focus:ring-2 focus:ring-blue-400 focus:border-transparent",
          className
        )}
        {...props}
      />
    );
  }
);
Input.displayName = "Input";