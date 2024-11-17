import { GameSettings } from './game-settings';

export const noLastDotCountValue = 0;

export const defaultCanvasColor = 'aliceblue';
export const defaultDotColor = 'hotpink';

export const minDotCount = 1;
export const maxDotCount = 1000;
export const dotCountStep = 1;

// In milliseconds
export const minShowDurationMs = 50;
export const maxShowDurationMs = 10000;
export const showDurationMsStep = 50;

export const nullGameSettings: GameSettings = { maxDotCount: 0, showDurationMs: 0 };
