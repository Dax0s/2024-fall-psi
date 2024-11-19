export function delay(ms: number): Promise<void> {
  return new Promise((resolve) => setTimeout(resolve, ms));
}

export class Timer {
  private startTime = 0;
  private endTime = 0;
  private running = false;

  public start() {
    this.startTime = this.currentTime();
    this.running = true;
  }

  public getTime(): number {
    return this.running ? this.currentTime() - this.startTime : this.endTime - this.startTime;
  }

  public stop() {
    if (!this.running) {
      return;
    }

    this.endTime = this.currentTime();
    this.running = false;
  }

  private currentTime(): number {
    return performance.now();
  }
}
