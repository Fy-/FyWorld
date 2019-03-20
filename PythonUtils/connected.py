# -*- coding: utf-8 -*-
"""
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
"""
from PIL import Image
import numpy as np

from utils.color import Color
from utils.texture2d import Texture2D
from utils.point import Point

class Connected(object):
	res = 128
	margin = 32

	def __init__(self, src):
		im = Image.open(src)
		self.pixels = im.load()	
		self.size = Point(im.size[0], im.size[1])
		self.texture_base = []

	def get_base(self):
		for i in range(0,4):
			self.texture_base.insert(i, Texture2D(Point(Connected.res, Connected.res)))
			for x in range(i*Connected.res, (i+1)*Connected.res):
				for y in range(0, Connected.res):
					self.texture_base[i].set_pixel(x-(i*Connected.res), y, self.get_pixel_from_base(x, y))	

	def gen(self):
		self.result = []
		#: 0
		self.result.insert(0, self.texture_base[0])

		#: 1
		tex = Texture2D(Point(Connected.res, Connected.res))
		tex = self.make_wall_tile(
			self.texture_base[2],
			self.texture_base[2],
			self.texture_base[0],
			self.texture_base[0]
		)
		self.result.insert(1, tex)

		#: 2
		tex = Texture2D(Point(Connected.res, Connected.res))
		tex = self.make_wall_tile(
			self.texture_base[1],
			self.texture_base[0],
			self.texture_base[1],
			self.texture_base[0]
		)
		self.result.insert(2, tex)

		#: 3
		tex = Texture2D(Point(Connected.res, Connected.res))
		tex = self.make_wall_tile(
			self.texture_base[3],
			self.texture_base[2],
			self.texture_base[1],
			self.texture_base[0]
		)
		self.result.insert(3, tex)

		#: 4
		tex = Texture2D(Point(Connected.res, Connected.res))
		tex = self.make_wall_tile(
			self.texture_base[0],
			self.texture_base[1],
			self.texture_base[0],
			self.texture_base[1]
		)
		self.result.insert(4, tex)

		#: 5
		tex = Texture2D(Point(Connected.res, Connected.res))
		tex = self.make_wall_tile(
			self.texture_base[2],
			self.texture_base[3],
			self.texture_base[0],
			self.texture_base[1]
		)
		self.result.insert(5, tex)

		#: 6
		self.result.insert(6, self.texture_base[1])


		#: 7
		tex = Texture2D(Point(Connected.res, Connected.res))
		tex = self.make_wall_tile(
			self.texture_base[3],
			self.texture_base[3],
			self.texture_base[1],
			self.texture_base[1]
		)
		self.result.insert(7, tex)

		#: 8
		tex = Texture2D(Point(Connected.res, Connected.res))
		tex = self.make_wall_tile(
			self.texture_base[0],
			self.texture_base[0],
			self.texture_base[2],
			self.texture_base[2]
		)
		self.result.insert(8, tex)

		#: 9
		self.result.insert(9, self.texture_base[2])

		#: 10
		tex = Texture2D(Point(Connected.res, Connected.res))
		tex = self.make_wall_tile(
			self.texture_base[1],
			self.texture_base[0],
			self.texture_base[3],
			self.texture_base[2]
		)
		self.result.insert(10, tex)

		#: 11
		tex = Texture2D(Point(Connected.res, Connected.res))
		tex = self.make_wall_tile(
			self.texture_base[3],
			self.texture_base[2],
			self.texture_base[3],
			self.texture_base[2]
		)
		self.result.insert(11, tex)

		#: 12
		tex = Texture2D(Point(Connected.res, Connected.res))
		tex = self.make_wall_tile(
			self.texture_base[0],
			self.texture_base[1],
			self.texture_base[2],
			self.texture_base[3]
		)
		self.result.insert(12, tex)

		#: 13
		tex = Texture2D(Point(Connected.res, Connected.res))
		tex = self.make_wall_tile(
			self.texture_base[2],
			self.texture_base[3],
			self.texture_base[2],
			self.texture_base[3]
		)
		self.result.insert(13, tex)

		#: 14
		tex = Texture2D(Point(Connected.res, Connected.res))
		tex = self.make_wall_tile(
			self.texture_base[1],
			self.texture_base[1],
			self.texture_base[3],
			self.texture_base[3]
		)
		self.result.insert(14, tex)

		#: 6
		self.result.insert(15, self.texture_base[3])

	def make_wall_tile(self, tl, tr, bl, br):
		unit = (int)(Connected.res/2)
		res = Texture2D(Point(Connected.res, Connected.res))

		# tl
		for x in range(0, unit):
			for y in range(0, Connected.margin):
				res.set_pixel(x, y, tl.get_pixel(x, y))

		# tr
		for x in range(unit, unit*2):
			for y in range(0, Connected.margin):
				res.set_pixel(x, y, tr.get_pixel(x, y))

		# bl
		for x in range(0, unit):
			for y in range(Connected.margin, Connected.res): # why 10 instead of Connected.margin ?
				res.set_pixel(x, y, bl.get_pixel(x, y))

		# br
		for x in range (unit, unit*2):
			for y in range(Connected.margin, Connected.res):
				res.set_pixel(x,y, br.get_pixel(x, y))

		return res

	def get_pixel_from_base(self, x, y):
		return np.array(self.pixels[x,y], dtype=np.uint8)

	def save(self):
		for i in range(0, 16):
			self.result[i].to_image().save("gen/mountain_%s.png" % i, format="png")

if __name__ == '__main__':
	cn = Connected("Test.png")
	cn.get_base()
	cn.gen()
	cn.save()