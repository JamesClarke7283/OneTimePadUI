from setuptools import setup

setup(
    name='One Time Pad UI',
    version='v2.0.0',
    packages=['UI', 'Core', 'Core.Crypto', 'Core.Crypto.Keys', 'Core.Crypto.Primitives', 'Core.Constants', 'Tests',
              'Tests.OTP', 'Tests.Keys', 'Tests.Primitives', 'Tests.Primitives.Vernam', 'Tests.Primitives.Conversion'],
    url='https://git.james-clarke.ynh.fr/james/OneTimePadUI',
    license='GPL3-or-later',
    author='James Clarke',
    author_email='james@james-clarke.ynh.fr',
    description='A Libre, One Time Pad program written in python and GTK for UI.'
)
