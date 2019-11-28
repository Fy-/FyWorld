# -*- coding: utf-8 -*-
"""
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
"""
import numpy as np

class Color(object):
	def __init__(self, r, g, b, a=255):


		self.data = np.array([int(r), int(g), int(b), int(a)], dtype=np.uint8)

	def dark_light(self, n):
		n = int(n*255)
		return Color(
			Color.add_255(self.data[0], n), 
			Color.add_255(self.data[1], n), 
			Color.add_255(self.data[2], n), 
			self.data[3])

	def dark_light_255(self, n):
		return Color(
			Color.add_255(self.data[0], n), 
			Color.add_255(self.data[1], n), 
			Color.add_255(self.data[2], n), 
			self.data[3])

	@staticmethod
	def from_array(arr):
		return Color(arr[0], arr[1], arr[2], arr[3])

	@staticmethod
	def compare(a, b, ignore_alpha=False):
		if (a[0] == b[0] and a[1] == b[1] and a[2] == b[2]) and (a[3] == b[3] or ignore_alpha):
			return True
		return False

	@staticmethod
	def add_255(c, n):
		t = c + n
		if t > 255:
			t = 255
		if t < 0:
			t = 0

		return t


Color.red = Color(255, 0, 0)
Color.green = Color(0, 255, 0)
Color.blue = Color(0, 0, 255)
Color.black = Color(0, 0, 0)
Color.white = Color(255, 255, 255)
Color.empty = Color(0, 0, 0, 0)
Color.pink = Color(80, 20, 60)
Color.orange = Color(90, 40, 10)
Color.grey = Color(127,127,127)

	