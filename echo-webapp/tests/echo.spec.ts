import { test, expect } from '@playwright/test';

test('Has Main Title', async ({ page }) => {
  await page.goto('/');

  const locator = page.locator('#mainTitle');
  await expect(locator).toHaveText('Echo WebApp');
});

test('Make Echo', async ({ page }) => {
  await page.goto('/');
  
  await page.getByRole('textbox').click();
  const str = (Math.random() * 10).toString(36).replace('.', '');
  await page.getByRole('textbox').fill(str);

  await page.getByRole('button', { name: 'Make Echo!' }).click();

  const locator = page.locator('#echoResult');
  const expected = `Echo: "${str}"`;
  await expect(locator).toHaveText(expected);
});