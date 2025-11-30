import React from "react";
import { cn } from "../lib/utils";

export const Label: React.FC<React.LabelHTMLAttributes<HTMLLabelElement>> = ({
  className,
  ...props
}) => (
  <label
    className={cn("text-sm font-medium text-gray-700", className)}
    {...props}
  />
);