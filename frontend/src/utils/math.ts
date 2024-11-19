export function inRange(value: number, minInclusive: number, maxInclusive: number): boolean {
  return minInclusive <= value && value <= maxInclusive;
}

export function sum(list: number[]): number {
  return list.reduce((a, b) => a + b, 0);
}

export function average(list: number[]): number {
  return sum(list) / list.length || Infinity;
}
