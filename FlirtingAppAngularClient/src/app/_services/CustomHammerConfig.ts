import { HammerGestureConfig } from '@angular/platform-browser';

export class CustomHammerConfig extends HammerGestureConfig {
    overrides = {
        pinch: { enable: false },
        rotate: { enable: false }
    };
}
