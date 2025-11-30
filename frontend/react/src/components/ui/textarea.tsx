import React from "react";
import { cn } from "../lib/utils";

export const Textarea = React.forwardRef<
  HTMLTextAreaElement,
  React.TextareaHTMLAttributes<HTMLTextAreaElement>
>(({ className, ...props }, ref) => {
  return (
    <textarea
      ref={ref}
      className={cn(
        "w-full px-3 py-2 border rounded-md bg-white text-gray-900 outline-none focus:ring-2 focus:ring-blue-400 focus:border-transparent",
        className
      )}
      {...props}
    />
  );
});
Textarea.displayName = "Textarea";
