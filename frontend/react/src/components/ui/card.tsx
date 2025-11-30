import React from "react";
import { cn } from "../lib/utils";

export function Card({ className, ...props }: React.HTMLAttributes<HTMLDivElement>) {
  return (
    <div
      className={cn(
        "rounded-xl border border-gray-200 bg-white shadow-sm p-4",
        className
      )}
      {...props}
    />
  );
}

export function CardTitle(props: React.HTMLAttributes<HTMLHeadingElement>) {
  return <h2 className="text-xl font-semibold mb-2" {...props} />;
}

export function CardContent(props: React.HTMLAttributes<HTMLDivElement>) {
  return <div className="mt-2" {...props} />;
}