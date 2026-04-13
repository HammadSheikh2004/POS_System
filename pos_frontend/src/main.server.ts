import { bootstrapApplication } from '@angular/platform-browser';
import { App } from './app/app';
import { config } from './app/app.config.server';
import { destroyPlatform } from '@angular/core';

destroyPlatform();
const bootstrap = () => bootstrapApplication(App, config);

export default bootstrap;
