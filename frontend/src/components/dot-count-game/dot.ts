import { Vec2 } from '@/utils/vec2';

export type Dot = {
  center: Vec2;
  radius: number;
};

export type DotCanvasInfo = {
  sideLength: number;
  dots: Dot[];
};
