# -*- coding: utf-8 -*-
"""
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
"""
class Point(object):
	def __init__(self, x, y):
		self.x = int(x)
		self.y = int(y)

	def divide(self, d):
		return Point(self.x/d, self.y/d)

	def half(self):
		return self.divide(2)

	def __str__(self):
		return "Point(%s,%s)" % (self.x, self.y)
		
	def copy(self):
		return Point(self.x, self.y)

	def __add__(self, other):
		return Point(self.x+other.x, self.y+other.y)

	def __sub__(self, other):
		return Point(self.x-other.x, self.y-other.y)

	def __len__(self):
		return abs(self.x+self.y)



Point.zero = Point(0,0)
Point.one = Point(1,1)